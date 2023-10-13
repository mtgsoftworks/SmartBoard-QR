# SmartBoard-QR
--Using--

Download the App to Your Android Device from the Play Store and Extract the PC Application RAR File to the Program Files Folder, then make the necessary adjustments and start using

Purpose of Use: Providing Windows Login with QR Code


Purpose: Windows operating system computer, tablet, smart board, etc.  Log in via phone by scanning the QR code on the device.


Method: The PC application is run as an administrator, the server address and password are entered from the settings section, and the general password is entered.  This feature is a backup password that can be used to log in to the system without using a QR code in case of a problem in the verification of the QR code. (You can log in by pressing Continue with Password.) All information is recorded and a QR code is created.  By opening the application on the phone, the necessary information (server address, server password, security code created in the PC application) is entered and the QR code is scanned.  Verification is provided on both platforms. (First, verification is made through the phone application. If the code is verified, that is, if it says verified in the verification section of the phone application, the QR code verification button is pressed, then by pressing the QR code verification button from the computer) The process is completed.

 The computer notifies you with a message whether the QR code is verified or not.

 Note: If verification is not done within 150 seconds, the PC will shut down suddenly.  If you try to shut down without verification from the task manager, your system will still be shut down for system security reasons.


 Working logic;  PC application server address, server password and security code texts are combined and encrypted.  A TCP (Transmission Control Protocol) server is created via Port 80.  To connect to this presentation, the phone decodes the encrypted QR code, parses the information in it and compares it with the data in the mobile application.  If the information provided matches exactly, the verification is successful.  Verification is done first from the phone and then from the computer.  If the process completes properly, the login is successful.


 Programming languages in which the PC application is written;  (C#, Visual Basic, JavaScript, HTML5, CSS3)


 Programming language used in the phone application: Java


 Result: With a single QR code, you can access both Windows operating system devices (computer, tablet, smart board), etc.  Login is provided as well as file transfer, PC snapshot and control between both platforms.


Here is the Mobile Application Download Link:
https://play.google.com/store/apps/details?id=com.mtgsoftworks.smartboardqr

