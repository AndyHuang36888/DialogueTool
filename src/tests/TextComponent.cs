using TextComponent = DialogueSystem.TextComponent;
using Xunit;

public class TextComponentTest
{
    [Fact]
    public void TestTextComponentConstructor()
    {
        string expectedId = "text1";

        // check correct id is set
        TextComponent textComponent = new TextComponent(expectedId);
        Assert.Equal(expectedId, textComponent.Id);;
    }

    [Fact]
    public void TestSetText()
    {
        string keyDog = "Dog Language";
        string keyCat = "Cat Language";
        string keyPython = "Snake Language";

        string valueDog = "Woof Woof";
        string valueCat = "Meow Meow";
        string valuePython = "Hiss Hiss";

        TextComponent textComponent = new TextComponent("text1");

        // test: add key val pair for empty TextComponent
        textComponent.SetText(keyDog, valueDog);
        Assert.Equal(valueDog, textComponent.GetText(keyDog));

        // test: 2 deep repetition for adding new key val pair
        textComponent.SetText(keyCat, valueCat);
        Assert.Equal(valueCat, textComponent.GetText(keyCat));

        // Add python language text
        textComponent.SetText(keyPython, valuePython);
        Assert.Equal(valuePython, textComponent.GetText(keyPython));

        valuePython = "I'm also a programming language!";

        // test: replace existing key val pair
        textComponent.SetText(keyPython, valuePython);
        Assert.Equal(valuePython, textComponent.GetText(keyPython));
    }

    [Fact]
    public void TestRemoveText()
    {
        string keyDog = "Dog Language";
        string valueDog = "Woof Woof";
        string keyCat = "Cat Language";
        string valueCat = "Meow Meow";

        TextComponent textComponent = new TextComponent("text1");

        // Add a text entries
        textComponent.SetText(keyDog, valueDog);
        textComponent.SetText(keyCat, valueCat);

        // Remove the text entry for Dog Language
        textComponent.RemoveText(keyDog);
        
        // Check if dodg language text is removed, 
        // check if it throws an exception
        Assert.Throws<KeyNotFoundException>(() => textComponent.GetText(keyDog));
        // Check if Cat Language text is still there
        Assert.Equal(valueCat, textComponent.GetText(keyCat));

        // check reptition for removing text
        textComponent.RemoveText(keyCat);
        Assert.Throws<KeyNotFoundException>(() => textComponent.GetText(keyCat));

    }
}