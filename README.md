## TOTP-AUTOBACKUP
# Make backups from totp-cli automatically

# How to install

# WARNING: Make sure installed totp-cli in 
`$HOME/go/bin` and added to PATH

For more information see:
https://github.com/yitsushi/totp-cli

Fedora:

`sudo dnf install ./totp-autobackup.1.0.0.rpm`

Debian:

`sudo apt install ./totp-autobackup.1.0.0.deb`

# Configuration

If you run totp-autobackup for first time the program creates the configuration automatically in `/home/$USER/.config/totp-autobackup/minute` `/home/$USER/.config/totp-autobackup/dir` and `/home/$USER/.config/totp-autobackup/password`
This files corresponds the configuration files like:
`dir`: It's the output directory for save the backups
`minute`: Every few minutes the backups will be saved.
`password`: Your password you writed in totp-cli for export the backups this write the password automatically for export the backups

In dir file open with text editor and write the path you want for save the backups.
In minutes: Set every x minute to save backups.

When the program detects the current time + minutes specified on `minute` file. Starts make the backups.

# How to enable?

`totp-autobackup --enable`

# How to disable?

`totp-autobackup --disable`
