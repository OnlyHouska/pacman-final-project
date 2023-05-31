@echo off

if not exist "MetaData" (
    mkdir MetaData
)

if not exist "MetaData/internal-settings.txt" (
    echo Creating $internal-settings
    (
    echo True
    echo 220
    echo False 
    echo ConsoleKey.W
    echo ConsoleKey.S
    echo ConsoleKey.A
    echo ConsoleKey.D
    echo ConsoleKey.Escape
    echo ConsoleKey.Backspace
    echo ConsoleKey.Enter
    echo ConsoleKey.F1
    echo ConsoleKey.S
    echo ConsoleKey.C
    echo ConsoleKey.Escape
    ) >> "MetaData/internal-settings.txt"
)

if not exist "MetaData/app-version" (
    echo Creating $app-version
    echo 0.14-053023 >> "MetaData\$app-version.txt"
)

if not exist "MetaData/tips.txt" (
    echo Creating tips.txt!
    (
    echo Press keys to move!
    echo Avoid the ghost!
    echo Turn off SFX. No really!
    echo Since 05/22/1980!
    echo PacMan has a GF!
    echo Power pellet is your friend!
    echo Collect all dots and fruits to win!
    echo Don't get greedy!
    echo The red ghost is Blinky!
    echo The ghost is dumb. Trust!
    echo UwU
    echo ^>-^<
    echo Nig... ht?
    echo Let's see how long a text here can be. It is probably bugged already. I don't know but this seems too much.
    echo Doesn't work on full screen.
    echo Also try Tic Tac Toe!
    echo Also try Space Invaders!
    echo Also try Hangman!
    echo Doesn't work on MaC! Lack of skill getting a Windows.
    echo This has a 5%% chance of showing up!
    ) >> "MetaData/tips.txt"
)

if not exist "MetaData/patch-notes" (
    REM cd MetaData
    REM curl -o $patch-notes https://raw.githubusercontent.com/OnlyHouska/pacman-project/main/$patch-notes?token=GHSAT0AAAAAACDHORPLDCVEFESSWNRD6L5OZDUWPEQ
)

pause
