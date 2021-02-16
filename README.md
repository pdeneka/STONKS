# FINRAREGSHO
Pulls down the FINRA RegSho data in http://regsho.finra.org/CNMSshvolYYYYMMDD.txt format.

# SOFTWARE IS USE AT YOUR OWN RISK
Seriously.  Not a financial advisor.  Don't know what I'm doing.  Don't ask me for help.  I'm an idiot.
Use at your own risk.
ETC ETC ETC

# DATA
FINRA provides aggregates of Short Volumes by Symbol and Market(s).  Each TXT file is a CSV+.
First line is headers.
N lines are data in CSV format with delimiter pipe ("|").
Last line is rowcount, excluding header and last line, for check.

For example, CNMSshvol20210129.txt (January 29, 2021) has 9299 rows total, and the last line is 9297. 9299 -1 (header) -1 (rows count) = 9297.


# SETUP
Set Line 30 to where you want to download the files.
Adjust Line 92 to tweak the sleep.
FINRA host is fast.  I downloaded 639 files in less than 10 minutes with 50ms sleep between pulls.  I'm sure it could be less, but don't abuse them.

# NOTES
FINRA occasionally updates the data.
You can check the ResultsFail check to include a stop date, and adjust the StartDate to whatever you want.

