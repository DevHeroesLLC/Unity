# Rules to follow when coding

1. Refactor when modifying
	The most important rule of them all. When you modify your code, don't forget to refactor it to conform to all of the rules we have.
	Don't change the code and leave refactoring for later. Later will never come. Remove redundant vars, check if naming is correct etc.
	Never leave commented code, because you think you might need it in the future. There are ways to get back to it when needed:
	* create a comment in code specifying when there was something that might be useful
	* create a tag/branch in git with this change

	It's possible that indeed you need to disable part of the code for some reason and you know it will be coming back. Comment it out
	and create a //TODO comment on top specifying why this is commented out and under what circumstances it will be coming back.

2. Use proper singletons!
	In case of MonoBehaviour scripts:
	```
	public class ExampleClass : MonoBehaviour {
		private static ExampleClass Instance = null;

		void Awake() {
			ExampleClass.instance = this;
		}

	```
	In case of regular classes:
	```
	public class ExampleClass {
		private static readonly ExampleClass instance = new ExampleClass();

		// almost-lazy, thread safe singleton
		static ExampleClass() {}

		private ExampleClass() {}

		public static ExampleClass Instance {
			get {
				return ExampleClass.instance;
			}
		}
	}
	```

3. Plugin management
	Before importing unity package into your project, create a new unity project, import the plugin there and refactor it's structure!
	All plugin stuff should be placed into `Assets/Plugins/<plugin name>/` directory! Make sure the plugin works after this and refactor further.
	After this, export plugin as unity package and send to Unitymodules repo!
	Please use plugin names or meaningful names as plugins directories, not names of the developer who created them!

4. Use proper directory structure!
	```
	Assets/
		Editor/ <- scripts which change inspector behaviour
		Plugins/ <- all plugins should be placed here in respectful directories
					please use plugins directory for stuff shared between projects as well!
		Scripts/ <- all project scripts grouped into directories, no script should be left in top Scripts directory
		Scenes/ <- projects scenes
			Unused/ <- any scenes which are not used in game (prototyping etc)
		Resources/ <- all prefabs/materials and such which are to be loaded in game, we use resources because it allows dynamic loading
				Materials/
						Road
						Wall
						...
				Prefabs/
				Shaders/
	```
	DO NOT CREATE TOP TIER CONTENT RELATED DIRECTORIES WHICH INCLUDES PREFABS/MATERIALS etc.

5. Use proper case for everything
	
	```
	Classes <- start with uppercase, always
	Scripts <- Start with uppercase, always
	classMethods <- Start with lowercase, use camelcase. The only exception are unity methods like Update etc. Boolean returning methods are preferred to start with is* prefix.
	classFields <- start with lowercase, use camelcase. Do not use underscores. Boolean variables should start with is prefix. isError, isRunning etc.
	```
6. Always use fields and methods qualifiers. DO NOT LEAVE PACKAGE qualifier on fields/methods. It's only allowed for Unity core methods.
7. Limit code nesting. Use inverted if statements to leave code blocks at start.
	
	```
	void something() {
		if (somethingElse) {
		    ...
		    //some code
		    ...
		}
	}
	```
	should become:
	```
	void something() {
	    if (!somethingElse) {
	    	return
	    }
	    ...
	}
	```
	Same for other blocks: 
	```
	for (...) {
		if (!isApplicable) {
			continue;
		}
	}
	```
8. Never create method parameter names the same as class fields.
9. Always think if your class have to extend MonoBehaviour. Maybe a regular class, a singleton is good enough.
10. Look out for `float` comparison and `int` division!
	`float` comparison should use epsilon
	`int` division returns integer, if not casted properly

11. Every class requires short summary of its purpose.
12. Every public method requires full description: summary, description, parameters description, return value description (use 3x / in Rider to generate template)
13. Every private method, which is not trivial requires at least summary (although full description is welcome)
14. Use object initializers where possible: `new SomeClass { fieldA = 1 }` etc.
15. Use switch instead of if where possible

	```
	if (transform.GetChild(i).name == "Socket") {
	    socket = transform.GetChild(i);
	} else if (transform.GetChild(i).name == "RightLane") {
	    RightLane = transform.GetChild(i).gameObject;
	} else if (transform.GetChild(i).name == "LeftLane") {
	    LeftLane = transform.GetChild(i).gameObject;
	}
	```

	instead:

	```
	switch (transform.GetChild(i).name) {
        case "Socket":
            socket = transform.GetChild(i);
            break;
        case "RightLane":
            RightLane = transform.GetChild(i).gameObject;
            break;
        case "LeftLane":
            LeftLane = transform.GetChild(i).gameObject;
            break;
    }
	```
16. Do not instantiate variables if value will never be used, use null!

	```
		int val = 123;
		if (isApplicable) {
		    val = 1;
		} else {
		    val = 3;
		}
	```
17. Use 180 characters per line. If your line is longer without function chaining, then refactor the code - move complex code into separate methods or create variables
	to hold results of calculations, instead of writing calculations directly in the method call.	

	Format chaining this way:
	```
	transform.GetChild(transform.childCount - ES2.Load<int>("MaterialID") - 1)
                    .GetComponent<RoadMaterialElement>()
                    .setThisMaterial();
	```
18. Conditionals `?:` can be used if they fit in 1 line and are brief and easy to understand. i.e. NOT:
	```
    Vector2 position = (_particleSystem.simulationSpace == ParticleSystemSimulationSpace.Local
        ? particle.position
        : _transform.InverseTransformPoint(particle.position));                
	``` 

    but:

	```
    var color = isRunning ? "green" : "red";
	```
19. Comments occupy separate line before comented code, not on the same line
20. `[Attributes]` for fields should be at the same line as fields, unless mroe than one `[Attribute]`, then each shouls have separate line and be preceded by a new line;
	
	```   
    [SomeAttribute] GameObject test;
	```

    or

	```
    [SomeAttribute]
    [SomeOtherAttribute]
    GameObject test;
	```

    The only exception: if the attribute is very long, then create it on separate line, i.e.
	```    
    [Tooltip("w parameter in each of elements is lenght of node offset.")]
    public List<Vector4> chunkSettings = new List<Vector4>();
	```
21. [Attributes] for methods and classes should have separate line
22. If scope contains variable declaration section, all variables of this scope should be declared there and it should be separated from the rest of the code by a single line.
23. If a result of a function is reused more than once within a scope it should be declared as variable! Especially Unity's `GetComponent`!
	
	```
	RandomObstaclesParamHolder.lastValue =
	        Mathf.Clamp(
	            tab[id].deltaPosition * pathLength +
	            Random.Range(GameSettings.obsMinDistance, GameSettings.obsMaxDistance), pathLength,
	            Mathf.Infinity) - pathLength;
	```	

	to:

	```
    float clampValue = tab[id].deltaPosition * pathLength + Random.Range(GameSettings.obsMinDistance, GameSettings.obsMaxDistance); 
    RandomObstaclesParamHolder.lastValue = Mathf.Clamp(clampValue, pathLength,Mathf.Infinity) - pathLength;
	```
24. Separate code chunks with a new line [for example, creation of an object which takes 5 lines should be separated from another piece of code by a single line]!
25. If for some bizzare reason, there is a need for an instruction to be wrapped into two lines [something that should be avoided at all cost] the next line should be indented TWICE! So 8 spaces, instead of 4.
	Same goes for if statements, however given 180 characters line limit, if this happens, it's probably a poor written code and should be refactored.
26. Use direct casting `(Type)` instead of `as Type`. It can be used only if you explicitly check [or expect] null values.
27. Know the difference between `someInt++` and `++someInt` and use it. i.e.
	
	```
    triangleIndices[ti] = a;
    ti++;
    triangleIndices[ti] = b;
	```

    could become:

	```    
    triangleIndices[ti++] = a;
    triangleIndices[ti++] = b;
	```
28. `yield` or `return yield` statement should always be preceeded and followed by a new line
29. In conditional statements, prefer to check for positive path rather than negative. In some cases it's just a matter of renaming a boolean and changing it's handling value to achieve that.
30. When referencing static methods or fields ALWAYS use the class name i.e. SomeClass.someStaticField instead of someStaticField
31. Know the difference between `class` & `struct`, and use structs only if it makes sense.
32. if you define variables which will never be modified, please define them as const
33. Don't create "obvious" if statements, i.e.
	
	```
	if (!reShow) {
        reShow = true;
    }
	```
    
    if it has to be true, just make it true:

	```    
    reShow = true
	```
34. do not use inline anonymous lambdas and delegates, unless they are extremaly simple and are never reused in code. Otherwise, please create a named lambda and reuse it.    
35. If you use invoke to delay a function call, maybe it's possible to use yield inside that function?
	
	```
	private void something() {
		yield return new WaitForSeconds(1)
	}
	```
36. create short, specialized methods - one method should do one thing and not be a mammoth which does everything.
37. classes should be specialized and do what they are supposed to do, not everything "around" the problem they were created to take care of.

