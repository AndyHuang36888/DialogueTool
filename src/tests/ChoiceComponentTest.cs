// using ChoiceComponent = DialogueSystem.ChoiceComponent;
// using Xunit;



// public class ChoiceComponentTest
// {
//     [Fact]
//     public void TestChoiceComponentConstrutor()
//     {
//         string expectedId = "choice1";

//         // check correct id is set  
//         ChoiceComponent choiceComponent = new ChoiceComponent(expectedId);
//         Assert.Equal(expectedId, choiceComponent.Text.ID);
//     }

//     [Fact]
//     public void TestSetText()
//     {
//         string keyDog = "Dog Language";
//         string keyCat = "Cat Language";

//         string valueDog = "Woof Woof";
//         string valueCat = "Meow Meow";

//         ChoiceComponent choiceComponent = new ChoiceComponent("choice1");   
//         // test: add key val pair for empty TextComponent
//         choiceComponent.SetText(keyDog, valueDog);
//         Assert.Equal(valueDog, choiceComponent.Text.GetText(keyDog));

//         // test: 2 deep repetition for adding new key val pair
//         choiceComponent.SetText(keyCat, valueCat);
//         Assert.Equal(valueCat, choiceComponent.Text.GetText(keyCat));
//     } 
// }