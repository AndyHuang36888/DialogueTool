using ChoiceComponent = DialogueSystem.ChoiceComponent;
using Xunit;



public class ChoiceComponentTest
{
    [Fact]
    public void TestChoiceComponentInitialization()
    {
        // Arrange
        string expectedId = "choice1";
        string expectedText = "This is a choice.";

        // Act
        var choiceComponent = new ChoiceComponent(expectedId, expectedText);

        // Assert
        Assert.Equal(expectedId, choiceComponent.Id);
        Assert.Equal(expectedText, choiceComponent.Text);
    }
}