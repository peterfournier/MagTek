<?xml version="1.0"?>
<package >
  <metadata minClientVersion="100.0.0.1">
	<id>Xamarin.Bindings.MagTek.Android</id>
	<version>1.0.1</version>        
	<title>Xamarin.Bindings.MagTek.Android</title>
    <authors>Peter Fournier</authors>
    <owners>Peter Fournier</owners>
    <licenseUrl></licenseUrl>
    <projectUrl>https://github.com/peterfournier/MagTek/tree/master/src/MTSCRA_Bindings.Android</projectUrl>
    <iconUrl></iconUrl>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
    <description>MagTek mtscra.jar bindings for Xamarin.Android</description>
	<language>en-US</language>
    <releaseNotes>Updated the MagTek SDK</releaseNotes>
    <copyright>Copyright 2016</copyright>
    <tags>MagTek Android Bindings</tags>
  </metadata>
  <files>
    <file src="Release\MTSCRA_Bindings.Android.dll" target="lib\NET40" />
  </files>
  
  <dependencies>
    <dependency id="Mono.Android" />
    <dependency id="System" />
	<dependency id="System.Core" />
</dependencies>
</package>