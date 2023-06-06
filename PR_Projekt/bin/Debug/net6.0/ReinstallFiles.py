from genericpath import isfile
import requests
import os

urls = [
    "https://raw.githubusercontent.com/OnlyHouska/pacman-final-project/main/PR_Projekt/bin/Debug/net6.0/MetaData/app-version.txt",
    "https://raw.githubusercontent.com/OnlyHouska/pacman-final-project/main/PR_Projekt/bin/Debug/net6.0/MetaData/internal-settings.txt",
    "https://raw.githubusercontent.com/OnlyHouska/pacman-final-project/main/PR_Projekt/bin/Debug/net6.0/MetaData/issues.txt",
    "https://raw.githubusercontent.com/OnlyHouska/pacman-final-project/main/PR_Projekt/bin/Debug/net6.0/MetaData/patch-notes.txt",
    "https://raw.githubusercontent.com/OnlyHouska/pacman-final-project/main/PR_Projekt/bin/Debug/net6.0/MetaData/tips.txt"
    #"https://download-directory.github.io?url=https://github.com/OnlyHouska/pacman-final-project/tree/main/PR_Projekt/bin/Debug/net6.0/MetaData/Audio"
]
names = [
    "app-version.txt",
    "internal-settings.txt",
    "issues.txt",
    "patch-notes.txt",
    "tips.txt"
    #"MetaData/Audio"
]

for url, name in zip(urls, names):
    pathToFile = "MetaData/" + str(name)
    if not os.path.isfile(pathToFile):
        response = requests.get(url)
        response.raise_for_status()

        with open(pathToFile, "wb") as file:
            file.write(response.content)

        print(f"File {name} downloaded successfully.")