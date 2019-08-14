using System;
using Foundation;
using AppKit;

namespace Galileo.Client.Mac
{
    /// <summary>
    /// Galileo Window
    /// </summary>
    public partial class MainWindow : NSWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Client.Mac.MainWindow"/> class.
        /// </summary>
        /// <param name="handle">Window Handle.</param>
        public MainWindow(IntPtr handle) : base(handle)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galileo.Client.Mac.MainWindow"/> class.
        /// </summary>
        /// <param name="coder">Coder Reference</param>
        [Export("initWithCoder:")]
        public MainWindow(NSCoder coder) : base(coder)
        {
        }

        /// <summary>
        /// Window's Awake Event
        /// </summary>
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }
    }
}
