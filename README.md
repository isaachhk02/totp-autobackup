## TOTP-AUTOBACKUP
# Make backups from totp-cli automatically

# How to install

Fedora:

`sudo dnf install ./totp-autobackup.1.0.0.rpm`

Debian:

`sudo apt install ./totp-autobackup.1.0.0.rpm`

# Configuration

If you run totp-autobackup for first time the program creates the configuration automatically in `/home/$USER/.config/totp-autobackup/minute` and `/home/$USER/.config/totp-autobackup/dir`
This files corresponds the configuration files like:
`dir`: It's the output directory for save the backups
`minute`: Every few minutes the backups will be saved.

In dir file open with text editor and write the path you want for save the backups.
In minutes: Set every x minute to save backups.

When the program detects the current time + minutes specified on `minute` file. Starts make the backups.

# How to enable?

`totp-autobackup --enable`

# How to disable?

`totp-autobackup --disable`
