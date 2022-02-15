#Software Engineering Capstone#

##Installation##

###Unity###
1. [Download Unity Hub](https://unity3d.com/get-unity/download)
2. [Create Unity Account](https://id.unity.com/en/conversations/7b1f1c15-625f-42bc-a038-c8547f9cb809018f) you can sign in with Google
3. Open the Unity Hub app
4. Navigate to the **Installs** tab on the Left 
5. Click on the **Install Editor** button on the top right corner of the screen
6. **Install Unity version 2020.3.28f1 (LTS)** which is the reccomended version
7. On the next screen you can use the default install settings (if you want to build for web install the WebGL build support, but you can easily do this step later)

###Source Control###
 1. make sure you are logged into Github in your browser. If you don't have a github account [create one GitHub Account now](https://github.com/signup?ref_cta=Sign+up&ref_loc=header+logged+out&ref_page=%2F&source=header-home) 
 2. [Create a GitKraken Account](https://app.gitkraken.com/login), you can use your Google account or prefereably your GitHub account to sign i
 3. [Download GitKraken GUI Installer](https://www.gitkraken.com/download/windows64) and run the installer 
 7. Open the GitKraken application
 8. Sign into your GitKraken Account preferably through your GitHub Account (if you did use your GitHub Account, skip to step 13)
 9. If you did not sign into GitKraken using GitHub, you need to link GitKraken to GitHub now.  
 10. In the GitKraken GUI menu bar navigate to File -> Preferences
 11. From the Preferences Screen, Navigate to the Integrations tab.
 12. Ensure is says your GitHub account is connected
 13. In the GitKraken GUI menu bar navigate to File -> Clone Repo
 14. Paste this Repository link directly into the URL box
 15. **Make sure to choose the path where you want the project to be located. *You will need this path later to initialize the Unity Project***
 16. press the **Clone the Repo!** button and wait for the repo to finish downloading, then press **open now**
 
###IDE###
1. [create or login to your Jetbrains account](https://account.jetbrains.com/login)
2. [Apply for Jetbrains Student License](https://www.jetbrains.com/shop/eform/students) if you were already approved for the GitHub Education Pack, you can use the GitHub tab to quickly get approved for the jetbrains student license
3. [Download the Rider Installer](https://www.jetbrains.com/rider/?_ga=2.6180787.1420589103.1644964244-1235206129.1644705957) and run the installer
4. After downloading and installing rider, simply run it and follow the on-screen prompts to sign in with your JetBrains Account to activate the student license

##Project Setup##
1. After completing the previous installations, open Unity Hub and navigate to the **projects tab** on the lefthand side of the window
2. From here click on the **Open** button at the top right
3. Navigate to the path where you cloned the repository.  There should be a subfolder here named **Elementals**, open this folder.  You should now see a number of folders one of them named **Assets**.  The assets folder is very important, so remember this path.
4. Press the Open button now, and wait while Unity sets up the project.  This may take a few minutes.
5. Once Unity finishes importing, the Unity Editor will open.  
6. From the Unity Editor navigate to **Edit -> Preferences** from the menu bar at the top of the screen
7. From the Preferences window, navigate to the **External Tools** tab on the left side of the preferences window
8. Next to **External Script Editor** click the dropdown menu and select **Rider** from the options
9. Now you are all ready to go!
