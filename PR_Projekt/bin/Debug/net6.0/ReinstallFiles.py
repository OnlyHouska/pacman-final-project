from genericpath import isfile
import requests
import os

urls = [
    "https://raw.githubusercontent.com/OnlyHouska/pacman-final-project/main/PR_Projekt/bin/Debug/net6.0/MetaData/app-version.txt",
    "https://raw.githubusercontent.com/OnlyHouska/pacman-final-project/main/PR_Projekt/bin/Debug/net6.0/MetaData/internal-settings.txt",
    "https://raw.githubusercontent.com/OnlyHouska/pacman-final-project/main/PR_Projekt/bin/Debug/net6.0/MetaData/issues.txt",
    "https://raw.githubusercontent.com/OnlyHouska/pacman-final-project/main/PR_Projekt/bin/Debug/net6.0/MetaData/patch-notes.txt",
    "https://raw.githubusercontent.com/OnlyHouska/pacman-final-project/main/PR_Projekt/bin/Debug/net6.0/MetaData/tips.txt"
]
names = [
    "MetaData/app-version.txt",
    "MetaData/internal-settings.txt",
    "MetaData/issues.txt",
    "MetaData/patch-notes.txt",
    "MetaData/tips.txt"
]

for url, name in zip(urls, names):
    pathToFile = "MetaData/" + str(name)
    if not os.path.isfile(pathToFile):
        response = requests.get(url)
        response.raise_for_status()

        with open(name, "wb") as file:
            file.write(response.content)

    print(f"File {name} downloaded successfully.")