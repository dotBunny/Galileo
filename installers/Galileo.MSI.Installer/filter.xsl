<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"

                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"

                xmlns:wix="http://schemas.microsoft.com/wix/2006/wi"

                xmlns="http://schemas.microsoft.com/wix/2006/wi"

                exclude-result-prefixes="xsl wix">



  <xsl:output method="xml" indent="yes" omit-xml-declaration="yes" />



  <xsl:strip-space elements="*"/>



  <xsl:key name="ExeToRemove"

           match="wix:Component[contains(wix:File/@Source, '.exe')]"

           use="@Id" />

  <xsl:key name="PDBToRemove"

          match="wix:Component[contains(wix:File/@Source, '.pdb')]"

          use="@Id" />

  <xsl:key name="XMLToRemove"

        match="wix:Component[contains(wix:File/@Source, '.xml')]"

        use="@Id" />

  <xsl:key name="LOGToRemove"

      match="wix:Component[contains(wix:File/@Source, '.log')]"

      use="@Id" />

  <xsl:key name="TXTToRemove"

    match="wix:Component[contains(wix:File/@Source, '.txt')]"

    use="@Id" />



  <xsl:template match="@*|node()">

    <xsl:copy>

      <xsl:apply-templates select="@*|node()"/>

    </xsl:copy>

  </xsl:template>



  <!-- Remove the exe files -->

  <xsl:template match="*[self::wix:Component or self::wix:ComponentRef]

                        [key('ExeToRemove', @Id)]" />

  <xsl:template match="*[self::wix:Component or self::wix:ComponentRef]

                        [key('PDBToRemove', @Id)]" />

  <xsl:template match="*[self::wix:Component or self::wix:ComponentRef]

                        [key('XMLToRemove', @Id)]" />

  <xsl:template match="*[self::wix:Component or self::wix:ComponentRef]

                        [key('LOGToRemove', @Id)]" />

  <xsl:template match="*[self::wix:Component or self::wix:ComponentRef]

                        [key('TXTToRemove', @Id)]" />

</xsl:stylesheet>