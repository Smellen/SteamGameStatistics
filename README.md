# SteamGameStatistics

This is an MVC application that displays the Steam data that is read in from a JSON file.
It's a work in progress.

### Usage guide
Before running the application the steam user key and the steam id need to be loaded into the environment variables.

1. Use a command line tool to clone the following repo.
    ```csharp
    git clone https://github.com/Smellen/SteamGameStatistics.git
    ```

2. Change directory to the following path:
    ```csharp
    cd .\SteamGameStatistics\src\SteamGameStatistics\
    ```

3. Run the following script to update the environment variables.
    1. The Steam id is the user unique identifier within Steam. It can be found by looking at the source on a Steam profile. To see user data the data privacy settings need to be Public.
    2. The Steam key is used to authentication requests to the Steam API. Keys can be aquired here with a Steam login: https://steamcommunity.com/dev/apikey

    ```csharp
    ./setSteamEnvironmentValues.ps1 <steamKey> <steamId>
    ```
4. Run the application.
    ```csharp
    dotnet run
    ```
