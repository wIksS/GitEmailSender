Git email sender

What you should do if you want to send emails on git commit.

1. Clone this repository.
2. Copy the post-commit file in it.
3. Put the copied file in : YourGitRepository/.git/hooks
4. Copy the EmailSender folder to your git local repository (folder)
5. Go again to the hooks folder. Open the post-commit file with notepad or notepad++
6. Edit the beggining of the last line from : "C:\Users\??????\Desktop\GitEmailSender\EmailSender\EmailSender\bin\Debug\EmailSender.exe" To "YourPathToYourGitFolder\EmailSender\EmailSender\bin\Debug\EmailSender.exe"
