using UnityEngine;                                                                  // make sure there are no unused things here!
																					// one line break between using and rest of the code
/// <summary>
/// This class does something cool													// always desribe what a class does
/// </summary>
public class SomeClass : MonoBehaviour {
    private static SomeClass instance;                                              // use proper singleton creation!
	public static int one = 1;
	public static two = 2;															// static variables are to be defined as first, singleton first
	private static int something = 0;                                               // NEVER declare fields between methods. fields declaration should be on top of the class
	private static int somethingElse = 1;                                           
                                                                                    // if possible, group public/private fields together
	[AttributeA] public int abcd = 1;												// single attribute should preceed field declaration on the same line
	public int abce = 2;

    [AttributeA]                                                                    // multiple attributes have their own line
    [AttributeB]                                                                       
	private int abc = 1;															// always explicitly declare fields as private, don't leave as protected
	private int abd = 2;
    private const int testConstant = 123;                                           // if value is never going to be modified, declare as constant!

    [Tooltip("w parameter in each of elements is lenght of node offset.")]          // very long single attributes should use separate line as well 
    public List<Vector4> chunkSettings = new List<Vector4>();

    /// <summary>																	// method comments are created by typing 3x/ in Rider
    /// Function does what it's name says it will									// EVERY public method requires full description (summary, params and return value)
    /// </summary>																	// private methods don't have to be commented this way
    /// <param name="parameterA">some param a</param>								// ONLY if they are brief and name of method and params make sense
    /// <param name="parameterB">some param b</param>
    /// <returns>some magic number</returns>
    [MethodAttribute]                                                               // method attributes always on separate line from method declaration
	public static int myFunction(TypeA parameterA, TypeB parameterB) {				// params are in same line with method declaration
        if (parameterA != "we only support this") {                                 // limit code nesting by inverting if statements
            return;
        }
		int funcVarA = 1;         													// method body starts in next line of method, unless preceeded by exit conditions
        float floatOne = 100.1f;                                                    // code blocks should start with variable declarations                          
        float floatTwo = 99.9f;                                                     // do not predeclare variables which are only used in nested code blocks
        int exampleInt = parameterB == WooHoo ? 1 : 10;                             // use ?: for very simple conditions  

		if (parameterA.something == funcVarA) {										// if/for/while/try etc. etc. are preceeded by 1 empty line
			someCodeHere();															// no line break after
		
		} else if (somethingElse == 10) {											// line break before new code block
			TypeC obj = new TypeC { paramA = 20 };									// if possible, declare method fields during construction of the object
			obj.woohoo();
			obj.boo();
			obj.ohYeah();
																					// comments are always put on separate line
			// doing something mysterious here 										// always comment things that may not be clear when someone else will look at it
			someOtherCode();
			somethingElse();
			SomeClass.one++;														// always use class when referencing static fields and methods!

            MyClass newInstance = (MyClass) generateNewInstance();                  // use type casting instead of 'as MyClass' unless you are prepared to handle nulls

			TypeD obj2 = new TypeD {												// logical blocks of code can be separated by a single line to make it more readable
				paramA = 20;
				paramB = 23;
				paramC = 34;
			}
			obj2.boo();																// no new line after closing brace
			objc2.woohoo();

			if (abcd == 1) {														// ALWAYS use braces
				abcd++;
			} else {
                int test = 123;                                                     // do not predeclare variables which are only used in nested code blocks
                int test123 = test++;                                               // know the difference between ++var and var++ and use appropriately!
                int test124 = test;
                int test125 = ++test;

				abcd--;
			}

			yield return nil														// line break before and after yield, unless yield is the last instruction of code block

            if (Math.Abs(floatOne - floatTwo) < 0.001) {
                Debug.Log("test");
            }

			switch (parameterB) {								                    // use switch statements instead of if when possible					
				case SomeValueB:
					obj.woohoo();
					break;
				default:
					obj2.boo();
			}
			blah();         
            transform.GetChild(transform.childCount - 1)                            // long method chaining should be divided into separate lines by method call
                    .GetComponent<RoadMaterialElement>()                            // only if it conforms with other rules (is it possible to make it simpler? clearer?)
                    .setThisMaterial();                                                         
            
            // one line comment should always be on it's own line                   // some comments about comments
            //TODO this is something that will be used after X days                 // TODO comments should describe something that is left to be done later, format is: //TODO <comment>
            /*
                Description of an algorithm                                         // mutliline comments can be used to describe specific logic within a function, however 
                                                                                    // it's better to create separate functions/methods for algorithms and describe the method properly
                It takes this and that in order to achieve something                // NEVER COMMIT COMMENTED CODE UNLESS PRECEEDED WITH //TODO comment 
                totally different                                                   // describing WHY it is left and when it's goint to be used!
            */
		}
		return 123;
	}

}



