# LigindaryAPI

![Total downloads](https://img.shields.io/github/downloads/LigindaLeg/LigindaryAPI/total?style=for-the-badge&logo=github)
![Latest Release](https://img.shields.io/github/v/release/LigindaLeg/LigindaryAPI?style=for-the-badge&logo=github)
![License](https://img.shields.io/github/license/LigindaLeg/LigindaryAPI?style=for-the-badge)

**LigindaryAPI** is an auxiliary library (API) created to expand the capabilities of plug—ins developed on the basis of **LabAPI**. Its goal is to provide developers with convenient and powerful tools to simplify common tasks such as managing custom effects, working with console commands, and interacting with players.

## Key Features

*   **🚀 A convenient API for commands:** Simplified registration and processing of console commands using clean and readable code.
* **🧙♂️ Custom effects management:** Easily create, give and remove custom effects for players.
*   **🛠️ Auxiliary methods:** A set of useful extension methods for working with data securely.
* **🔌 Extensibility:** The API is designed to be easy to add new features to.

## Installation

### For server owners

1. Go to [**Releases**](https://github.com/LigindaLeg/LigindaryAPI/releases ).
2. Download the latest version `
LigindaryAPI-LabAPI.dll 3. Place the downloaded file in the plug-in folder of your server.

## Usage examples

Below is an example demonstrating the API features.

### Create custom effects

Create a command handler that will manage the logic of issuing, deleting, and viewing effects.

```csharp
public class ExampleEffect : CustomEffect
{
    public string Name => "ExampleEffect";

    public string Description => "Example of a custom effect";
    
    public void OnGive(Player player, float duration)
    {
// Insert the effect functions that occur when the effect is applied here
}

    public void OnRemove(Player player)
    {
         // Insert here the effect functions that occur when the effect is removed
}
}
```

## API Help (Short)

| Method / Class | Description |
| :--- | :--- |
| ` CustomEffect.GiveToPlayer(player, duration)` | Gives the effect to the player for a set duration. |
| `Player.Hint(text, duration, y, x)` | Shows the player the text on the screen (Attention! For this function, you need to have [HintServiceMeow](https://github.com/MeowServer/HintServiceMeowhttps://github.com/MeowServer/HintServiceMeow)). |
| `RegisterEffect(CustomEffect)` | Registers a new custom effect in the system. |
| `string.IsCustomEffect()` | Checks if there is an effect with the same name. |
| `string.ToCustomEffect()` | Converts the name of the effect into its object. |

## How to contribute

We welcome any contribution to the development of the project! If you want to help:

1. **Report errors:** If you find a bug, please create a new [Issue](https://github.com/LigindaLeg/LigindaryAPI/issues ) with a detailed description.
2. **Offer ideas:** Do you have an idea for a new feature? Create an [Issue](https://github.com/LigindaLeg/LigindaryAPI/issues ) and describe it.
3. **Send Pull Requests:**
    * Make a fork of the repository.
    * Create a new branch (`git checkout -b feature/AmazingFeature').
    * Make your changes.
    * Commit the changes ('git commit -m 'Add some amazing Feature').
    * Submit changes to your branch ('git push origin feature/AmazingFeature').
    * Open **Pull Request**.

## License

This project is licensed under a Creative Commons license. For more information, see the `LICENSE` file.
