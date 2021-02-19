# SOFTWARE IS USE AT YOUR OWN RISK
Seriously.  Not a financial advisor.  Don't know what I'm doing.  Don't ask me for help.  I'm an idiot.
Use at your own risk.
ETC ETC ETC

I did this because I got tired of doing this shit by hand.


# FINRAREGSHO
Pulls down the FINRA RegSho data in http://regsho.finra.org/CNMSshvolYYYYMMDD.txt format.

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


# SEC FTD
Pulls down the SEC Failure To Deliver data in the following formats:

  https://www.sec.gov/files/data/fails-deliver-data/cnsfails202101a.zip
  
  https://www.sec.gov/files/data/fails-deliver-data/cnsfails202101b.zip

# DATA
SETTLEMENT DATE: YYYYMMDD

CUSIP: Customer ID?  Not sure.

SYMBOL: Stock

QUANTITY (FAILS): How many shares the [DTCC???] failed to deliver

DESCRIPTION: Human readable stock name

PRICE: USD

# DATA VALIDATION
SEC uses the same format as FINRA above, with an additional trailing row, for validation:
Row 1: Headers
Rows 2 to N-2: data
Row N-1: Trailer record count X
Row N: Total Shares


# NOTES
SEC does not garauntee accuracy and/or delivery date.
SEC can update the files as necessary.
Do your own homework.
