using TextComponent = DialogueSystem.TextComponent;
using Xunit;

class TextComponentTest
{
    [Fact]
    public void TestTextComponentInitialization()
    {
        // Arrange
        string expectedId = "text1";
        string expectedText = "Hello, world!";

        // Act 
        TextComponent textComponent = new TextComponent(expectedId, expectedText);

        // Assert
        Assert.Equal(expectedId, textComponent.Id);
        Assert.Equal(expectedText, textComponent.Text);
    }
}