using System;
using System.Collections.Specialized;
using System.Windows.Forms;

public class ModInfo
{
	private bool isInstalled;
	private bool controlsSet = false;
	private string name;
	private string apiURL;
	private string installedVersion = "(none)";
	private string newestVersion = "";
	private string zipLocation = "";
	private StringCollection fileList, folderList;

	private TextBox ctrlName, ctrlInstalledVersion, ctrlNewestVersion;
	private Button btnInstall, btnRemove;

	public ModInfo(string name, bool isInstalled, string apiURL, string installedVersion, StringCollection fileList, StringCollection folderList)
	{
		this.name = name;
		this.isInstalled = isInstalled;
		this.apiURL = apiURL;
		this.installedVersion = installedVersion;
		this.fileList = fileList;
		this.folderList = folderList;
		UpdateControls();
	}

	public void SetNewestVersion(string version)
	{
		this.newestVersion = version;
		UpdateControls();
	}

	public string GetNewestVersion()
	{
		return newestVersion;
	}

	public void SetIsInstalled(bool isInstalled)
    {
		this.isInstalled = isInstalled;
		UpdateControls();
	}

	public void SetInstalledVersion(string installedVersion)
    {
		this.installedVersion = installedVersion;
		UpdateControls();
	}

	public void SetZipLocation(string zipLocation)
    {
		this.zipLocation = zipLocation;
		UpdateControls();
	}

	public string GetZipLocation()
    {
		return zipLocation;
    }

	public void SetFileList(StringCollection fileList)
	{
		this.fileList = fileList;
		UpdateControls();
	}

	public StringCollection GetFileList()
	{
		return this.fileList;
	}

	public void SetFolderList(StringCollection folderList)
	{
		this.folderList = folderList;
		UpdateControls();
	}

	public StringCollection GetFolderList()
	{
		return this.folderList;
	}

	public void SetControls(TextBox ctrlName, TextBox ctrlInstalledVersion, TextBox ctrlNewestVersion, Button btnInstall, Button btnRemove)
    {
		controlsSet = true;

		this.ctrlName = ctrlName;
		this.ctrlInstalledVersion = ctrlInstalledVersion;
		this.ctrlNewestVersion = ctrlNewestVersion;
		this.btnInstall = btnInstall;
		this.btnRemove = btnRemove;

		UpdateControls();
	}

	private void UpdateControls()
    {
		if (!controlsSet)
			return;

		ctrlName.Text = name;
		ctrlInstalledVersion.Text = installedVersion;
		ctrlNewestVersion.Text = newestVersion;
		btnInstall.Enabled = !isInstalled;
		btnRemove.Enabled = isInstalled;
	}
}
