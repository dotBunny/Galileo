using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Galileo.Core.FileTypes;

namespace Galileo.Core.Submissions
{
    [Flags]
    enum SubmissionCandidateResolution
    {
        /// <summary>
        /// SubmissionCandidate is a submission for procesing
        /// </summary>
        Submission = 1,

        /// <summary>
        /// SubmissionCandidate is actually a directory, it may of
        /// been scanned and other SubmissionCandidates added
        /// </summary>
        Directory = 2,

        /// <summary>
        /// SubmissionCandidate is actually an archive, it may of
        /// been scanned and other SubmissionCandidates added.
        /// </summary>
        Archive = 4,

        /// <summary>
        /// Future:
        /// Submission is marked as old, because there is a newer
        /// submission found by the same author.
        /// </summary>
        OlderSubmission = 8,

        /// <summary>
        /// Blocked by a filter
        /// </summary>
        Filtered = 16,

        /// <summary>
        /// The submission has been un-zipped, or copied into a temporary folder
        /// for Submission class to look at.
        /// </summary>
        Exported = 64,


        IgnoreButExport = 128,

        /// <summary>
        /// Is Resolved
        /// </summary>
        Resolved = 256,

        None = 0
    }




    internal class SubmissionCandidate : IDisposable
    {

        #region Fields
   
        private bool _disposed = false;
   
        /// <summary>
        /// What if any information was found about the submission candidate
        /// </summary>
        private SubmissionCandidateResolution _resolution;

        /// <summary>
        /// What if any is the file type
        /// </summary>
        private FileType _fileType;

        /// <summary>
        /// If it's a file, what is it's name.
        ///   > assignment.docx
        /// </summary>
        private string _fileName;

        /// <summary>
        /// If it's a file, what is it's name.
        ///   > assignment
        /// </summary>
        private string _fileNameWithoutExtension;

        /// <summary>
        /// If it's a file, what is it's extension (without the docx)
        ///   > docx
        /// </summary>
        private string _fileExtension;

        /// <summary>
        /// If it's a file, what is the FileSystem/Archive path of that file.
        ///   > C:/Users/Robin/Desktop/Assignments/CS102/                   [When Directory]
        ///   > C:/Users/Robin/Desktop/Assignments/CS102/submissions.zip    [When Archive]
        /// </summary>
        private string _containerPath;

        /// <summary>
        /// What is the path of the submission candidate
        /// </summary>
        private string _path;

        /// <summary>
        /// If the submission doesn't have a true path (i.e. a file in archive)
        /// where is a copy of the submission on a FileSystem (i.e. unzipped)
        /// </summary>
        private string _resolvePath;

        /// <summary>
        /// Does the submission candidate have a parent container?
        /// e.x.
        ///     FileSystem
        ///     Archive
        ///     Document format containing pictures, video, etc.
        /// </summary>
        private SubmissionCandidate _parent;

        /// <summary>
        /// Unique Id of the SubmissionCandidate
        /// </summary>
        private Guid _guid;
        
        /// <summary>
        /// Get the a shorthand unique if of the submission
        /// e.x.
        ///     930C7802-8A8C-48F9-8165-68863BCCD9DD -> 930C780
        /// </summary>
        private string _guidShorthand;

        /// <summary>
        /// Current recursive depth
        /// </summary>
        private int _depth;

        /// <summary>
        /// File Information IF it is a non-filtered submission
        /// </summary>
        private System.IO.FileInfo _fileInfo;

        /// <summary>
        /// Archive Information and extractor IF it is a archive-file submission
        /// </summary>
        IArchiveInfo _archiveInfo;

        /// <summary>
        /// All candidates in an archive, but not any candidates in an archive
        /// which is in that archive.
        /// </summary>
        List<SubmissionCandidate> _archiveChildren;
        
        #endregion

        /// <summary>
        /// Constructor for Submission Candidate
        /// </summary>
        /// <param name="path">Path to the candidate</param>
        /// <param name="depth">Current filesystem depth</param>
        /// <param name="parent">Any parent submission</param>
        public SubmissionCandidate(string path, int depth, SubmissionCandidate parent)
        {
            _guid = Guid.NewGuid();
            _guidShorthand = _guid.ToString("N").Substring(0, 6);
            _resolution = SubmissionCandidateResolution.None;
            _depth = depth;
            _path = path;
            _parent = parent;
            _fileType = FileTypes.Types.None;
            _resolvePath = string.Empty;

            // Initial set of naming information, before processing or exploding, useful
            // for any sort of filtering that gets done prior to exploding
            _fileName = Path.GetFileName(path);
            _fileNameWithoutExtension = Path.GetFileNameWithoutExtension(_fileName);
            _fileExtension = Path.GetExtension(_fileName);
        }
        
        public void Dispose()
        { 
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return; 

            if (disposing)
            {
                if (_archiveInfo != null)
                {
                    _archiveInfo.Close();
                }
            }
            
            _disposed = true;
        }

        #region Properties

        /// <summary>
        /// Get the parent submission which is usually a Directory or Archive
        /// </summary>
        public SubmissionCandidate Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Get the current filesystem depth of the submission
        /// </summary>
        public int Depth
        {
            get { return _depth; }
        }

        /// <summary>
        /// Get the unique id of the submission
        /// </summary>
        public Guid Guid
        {
            get { return _guid; }
        }

        /// <summary>
        /// Get the a shorthand unique of the Guid
        /// </summary>
        public string GuidShorthand
        {
            get { return _guidShorthand; }
        }

        /// <summary>
        /// Get the resolution of the candidate once resolved
        /// </summary>
        public SubmissionCandidateResolution Resolution
        {
            get { return _resolution; }
        }

        /// <summary>
        /// Get the file type of the candidate once resolved.
        /// Otherwise it will be a:
        ///         'application/octect-stream' or 'None'
        /// Incase of a unsupported file type, or if the candidate
        /// is a directory.
        /// </summary>
        public FileType FileType
        {
            get { return _fileType; }
        }

        /// <summary>
        /// Get the path of the submission where the file can
        /// be read. Either this is the true path of the submission
        /// or in the case of an archived file, an uncompressed temporary
        /// copy of the file.
        /// </summary>
        public String ReadPath
        {
            get
            {
                if (String.IsNullOrEmpty(_resolvePath))
                {
                    return _path;
                }
                else
                {
                    return _resolvePath;
                }
            }
        }


        public IArchiveInfo ArchiveInfo
        {
            get
            {
                if (_archiveInfo != null)
                    return _archiveInfo;
                if (Parent != null && Parent.IsArchive)
                {
                    return Parent.ArchiveInfo;
                }
                return null;
            }
        }

        /// <summary>
        /// Does the candidate have a parent such as a Directory or Archive
        /// </summary>
        public bool HasParent
        {
            get { return (_parent != null); }
        }

        /// <summary>
        /// Is the candidate a file?
        /// </summary>
        public bool IsFile
        {
            get { return (_parent != null && _parent.IsDirectory) && !IsDirectory; }
        }

        /// <summary>
        /// Is the candidate a file in an archive?
        /// </summary>
        public bool IsArchivedFile
        {
            get { return (_parent != null && (_parent.IsArchive || _parent.IsArchivedFile)); }
        }

        /// <summary>
        /// Has the candidate gone through the resolve process?
        /// </summary>
        public bool IsResolved
        {
            get { return (_resolution & SubmissionCandidateResolution.Resolved) != 0; }
        }

        /// <summary>
        /// Is the candidate a directory?
        /// </summary>
        public bool IsDirectory
        {
            get { return (_resolution & SubmissionCandidateResolution.Directory) != 0; }
        }

        /// <summary>
        /// Is the the candidate an archive?
        /// </summary>
        public bool IsArchive
        {
            get { return (_resolution & SubmissionCandidateResolution.Archive) != 0; }
        }

        /// <summary>
        /// Is the candidate a container; A directory or archive? 
        /// </summary>
        public bool IsContainer
        {
            get { return (IsDirectory || IsArchive); }
        }

        /// <summary>
        /// Is the candidate a supported file that can be processed?
        /// </summary>
        public bool IsSubmission
        {
            get { return (_resolution & SubmissionCandidateResolution.Submission) != 0; }
        }

        /// <summary>
        /// Should the candidate be ignored due to a filter blacklisting this
        /// particular candidate?
        /// </summary>
        public bool IsFiltered
        {
            get { return (_resolution & SubmissionCandidateResolution.Filtered) != 0; }
        }

        /// <summary>
        /// Is the candidate an older version of another candidate?
        /// </summary>
        public bool IsOlderSubmission
        {
            get { return (_resolution & SubmissionCandidateResolution.OlderSubmission) != 0; }
        }

        /// <summary>
        /// If the candidate is an archived file, has a temporary copy been made?
        /// </summary>
        public bool IsExported
        {
            get { return (_resolution & SubmissionCandidateResolution.Exported) != 0; }
        }
        
        /// <summary>
        /// We should ignore the file but the file should be exported if in an archive
        /// </summary>
        public bool IsIgnoredButExport
        {
            get { return (_resolution & SubmissionCandidateResolution.IgnoreButExport) != 0; }
        }

        /// <summary>
        /// The size of the file in bytes, or if a directory then 0 is returned.
        /// </summary>
        public long FileSize
        {
            get { return _fileInfo != null ? _fileInfo.Length : 0; }
        }

        /// <summary>
        /// The name of the file or directory including an extension (if any)
        ///     e.x.
        ///         CS102-James-Williams.docx
        ///         CS102-Submissions
        /// </summary>
        public String FileName => _fileName;

        /// <summary>
        /// The name of the file or directory without an extension (if any)
        ///     e.x.
        ///         CS102-James-Williams
        ///         CS102-Submissions
        /// </summary>
        public String FileNameWithoutExtension => _fileNameWithoutExtension;

        /// <summary>
        /// The path of the container owning this submission
        ///     e.x.
        ///         C:/users/cs102/Desktop/CS102-Submissions
        ///         C:/users/cs102/Desktop/cs102.zip
        /// </summary>
        public String ContainerPath => _containerPath;

        /// <summary>
        /// The extension of the file, or blank if a directory
        ///     e.x.
        ///         docx
        ///         pdf
        /// </summary>
        public String FileExtension => _fileExtension;

        /// <summary>
        /// The date and time of the file was written to, or DateTime.MinValue if
        /// a directory or archive.
        /// </summary>
        public DateTime FileWriteTime
        {
            get { return _fileInfo != null ? _fileInfo.LastWriteTime : DateTime.MinValue; }
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Search a directory or archive and add any files or sub-directories
        /// to the list of candidates currently being resolved
        /// </summary>
        /// <param name="candidates">Candidates currently being resolved</param>
        public void Explode(SubmissionCandidates candidates, string workingDirectory)
        {
            if (IsArchive)
            {
                // Archives in Archives!!
                if (IsArchivedFile)
                {
                    string exportPath = Path.Combine(workingDirectory, HunterConfig.GalileoDefaultDataFolder + Path.DirectorySeparatorChar + "galileo_" + Guid.ToString() + FileExtension);
                    
                    ArchiveInfo.Extract(ReadPath, exportPath);
                    _resolvePath = exportPath;
                }

                if (_fileType == FileTypes.Types.Zip)
                {
                    _archiveInfo = new ZipArchiveInfo(ReadPath);
                    _archiveChildren = new List<SubmissionCandidate>(10);

                    if (_archiveInfo.IsOpen == false)
                    {
                        // Probably best to do something better than this?
                        throw new Exception("Could not open archive???");
                    }

                    foreach(var fileName in _archiveInfo)
                    {
                        SubmissionCandidate candidate = candidates.Add(fileName, _depth + 1, this);
                        _archiveChildren.Add(candidate);
                    }

                    // We leave _archiveInfo open, because later the files
                    // will want to be inspected and possibly extracted.
                }
            }
            else if (IsDirectory)
            {
                System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(ReadPath);
                foreach (var file in info.EnumerateFiles())
                {
                    candidates.Add(file.FullName, _depth + 1, this);
                }
                foreach (var directory in info.EnumerateDirectories())
                {
                    candidates.Add(directory.FullName, _depth + 1, this);
                }
            }
        }

        /// <summary>
        /// Resolve this candidate and work out what it is; A File, Directory or Archive.
        /// Once that it is checked against the filters to see if it can be processed
        /// any futher.
        /// 
        /// If it's a file then the type is worked out.
        /// If it's a directory or archive, then it may be 'exploded' so sub-directories
        /// and files will be added as candidates.
        /// 
        /// After all of that _resolution contains the resolved state of the Candidate.
        /// </summary>
        /// <param name="candidates">Candidates currently being resolved</param>
        /// <returns></returns>
        public SubmissionCandidateResolution Resolve(SubmissionCandidates candidates, string workingDirectory)
        {
#if DEBUG
            Console.WriteLine(ReadPath);
#endif

            // Start off with nothing
            _resolution = SubmissionCandidateResolution.None;

            bool isValidFile = false, isArchive = false, isFileSystem = false, isInArchive = false, isIgnoredButExported = false;
            bool shouldExplode = false;


            // @TODO Detect parents, etc.
            if (Parent != null)
            {
                if (Parent.IsArchive)
                {
                    isInArchive = true;
                }
            }

            if (isInArchive == false)
            {
                // Is this a directory?
                if (FileTypes.Types.IsFileSystem(ReadPath))
                {
                    // Yes, it's a directory.
                    isFileSystem = true;
                    _containerPath = ReadPath;
                }
                else
                {
                    // What about an Archive?
                    if (FileTypes.Types.IsArchive(ReadPath, out _fileType))
                    {
                        isArchive = true;
                    }
                    // Then it must be a file.
                    // But before we start processing, we need to check to
                    // see if it's a supported file. If not then it's ignored.
                    else if (FileTypes.Types.TryGet(ReadPath, out _fileType))
                    {
                        if (_fileType != FileTypes.Types.OctetStream)
                        {
                            isValidFile = true;
                        }
                    }
                }
            }
            else
            {
                // What if it's an archive in an archive?? - That's just crazy talk!!
                if (FileTypes.Types.IsArchiveInArchive(ReadPath, ArchiveInfo, out _fileType))
                {
                    isArchive = true;
                }
                else
                {
                    if (FileTypes.Types.TryGetInArchive(ReadPath, ArchiveInfo, out _fileType))
                    {
                        if (_fileType == FileTypes.Types.OctetStream)
                        {
                            isIgnoredButExported = true;
                        }
                        else
                        {
                            isValidFile = true;
                        }
                    }
                    else
                    {
                        isIgnoredButExported = true;
                    }
                }
            }


            // Is it an archive?
            // Mark it as an archive, and put up a flag so it can be exploded later.
            if (isArchive)
            {
                _resolution |= SubmissionCandidateResolution.Archive;
                shouldExplode = true;
            }
            // Is it a directory
            // Mark it as a directory, and put up a flag so it can be exploded later
            else if (isFileSystem)
            {
                _resolution |= SubmissionCandidateResolution.Directory;

                shouldExplode = true;
            }
            // Is it a valid file?
            // Then it's an submission
            else if (isValidFile)
            {
                _resolution |= SubmissionCandidateResolution.Submission;
            }
            
            if (isIgnoredButExported)
            {
                _resolution |= SubmissionCandidateResolution.IgnoreButExport;
            }

            // Apply the filters now
            bool filtered = false;
            
            // Hotfix for now, since .zip files are being applied as no-nos by the config
            if (isArchive == false)
            {
                filtered = candidates.ApplyFilter(this);
            }

            if (filtered)
            {
                _resolution |= SubmissionCandidateResolution.Filtered;
            }
            
            // If we need to explode and we aren't filtered, then explode!
            if (shouldExplode && filtered == false)
            {
                Explode(candidates, workingDirectory);
            }

            // Finally mark as resolved
            _resolution |= SubmissionCandidateResolution.Resolved;

            return _resolution;
        }


        /// <summary>
        /// Try and create a submission based on the current candidate, it will only do
        /// this if the following conditions are met.
        ///     - Has to be resolved through the Resolve function
        ///     - Not a Directory
        ///     - Not an Archive
        ///     - Is a supported Submission file
        ///     - Not an unknown file type
        /// If the file is in an archive it may be uncompressed to a temporary folder
        /// for reading.
        /// </summary>
        /// <param name="session">The current session</param>
        /// <param name="submission">The a reference to the submission to return as</param>
        /// <returns></returns>
        public bool TryCreateSubmission(HunterSession session, out Submission submission)
        {

            // If we were unable to resolve, if its a directory, if its an archive, if its not a valid submission, 
            // or if we just dont know the file type (unsupported), don't even bother.
            if (IsResolved == false || IsDirectory || IsArchive || !IsSubmission || FileType.Kind == FileKind.Unknown)
            {
                submission = null;
                return false;
            }

            // @TODO Resolve if in archive
            if (IsArchivedFile)
            {
                if (Parent.IsExported == false)
                {
                    Parent.ExportFile(session.WorkingDirectory, false, session.Config.ProcessArchivesExtractOnlySubmissions);
                }

                ExportFile(Parent.ReadPath);
            }
            else
            {
                ExportFile(session.WorkingDirectory);
            }

            submission = new Submission(session, this);

            return true;
        }
        
        /// <summary>
        /// Export the file for processing, that if it is in an archive. Additionally
        /// final checks on the file so they are ready to become a submission
        /// </summary>
        protected void ExportFile(string workingDirectory, bool ignoreParent = false, bool onlySubmissions = false)
        {
            if (IsExported)
                return;

            _resolution |= SubmissionCandidateResolution.Exported;

            if (ignoreParent == false && Parent != null)
            {
                Parent.ExportFile(workingDirectory, ignoreParent, onlySubmissions);
            }

            if (IsArchive && _archiveChildren != null)
            {
                // Design path here. Using parents if any.
                
                string basePath = string.Empty;

                if (HasParent)
                {
                    // archive in a folder
                    if (Parent.IsDirectory)
                    {
                        basePath = Path.Combine(workingDirectory, HunterConfig.GalileoDefaultDataFolder);
                    }
                    // archive in a archive
                    else
                    {
                        basePath = Parent.ReadPath;
                    }
                }
                else
                {
                    basePath = Path.Combine(workingDirectory, HunterConfig.GalileoDefaultDataFolder);
                }


                //string basePath = Path.Combine(workingDirectory, HunterConfig.GalileoDefaultDataFolder);
                _resolvePath = Path.Combine(basePath, string.Format("A_{0}_{1}", GuidShorthand, FileName));
                
                // Create folder here.
                Directory.CreateDirectory(ReadPath);

                if (onlySubmissions == false)
                {
                    foreach(var child in _archiveChildren)
                    {
                        if (child.IsIgnoredButExport)
                        {
                            child.ExportFile(ReadPath, true, onlySubmissions);
                        }
                    }
                }
            }

            if ((IsSubmission || IsIgnoredButExport) && !IsFiltered)
            {
                // @TODO
                //    If in an archive, then export file to readpath.
                if (IsArchivedFile)
                {
                    _fileName = System.IO.Path.GetFileName(ReadPath);
                    _fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(ReadPath);
                    _fileExtension = System.IO.Path.GetExtension(ReadPath);
                    
                    string[] paths = Platform.SplitArchivePath(ReadPath);
                    paths = Platform.CleanArchivePaths(paths);

                    string exportPath = Path.Combine(workingDirectory, Path.Combine(paths));
                    Directory.CreateDirectory(Path.GetDirectoryName(new DirectoryInfo(exportPath).FullName));

                    ArchiveInfo.Extract(ReadPath, exportPath);
                    
                    _fileInfo = new System.IO.FileInfo(exportPath);
                    _resolvePath = exportPath;
                }
                else
                {
                    _fileInfo = new System.IO.FileInfo(ReadPath);

                    // Update with new information based on finalized pathing information
                    _fileName = System.IO.Path.GetFileName(ReadPath);
                    _fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(ReadPath);
                    _fileExtension = System.IO.Path.GetExtension(ReadPath);
                }
                _containerPath = Parent.ContainerPath;
            }

        }

        #endregion
    }
}
