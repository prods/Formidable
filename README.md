FORMIDABLE
------------------

### Status:
In development...but working...

### Requirements
* Visual Studio 2017. But it should be compatible with 2015.
* .NET 4.0+

### Introduction
Formidable is a simple MVVM framework for .NET Windows Forms solutions that aims to provide a _simple_ way of isolating your State and Business logic from your Presentation logic while reinforcing re-usability and refactoring.

### Why Windows Forms? Isn't it dead already?...
Well, this is a good question. Even if the tendency is for Windows Forms to die the slow death of platform-specific technologies used to build Pyramids, in the Javascript era, it is still being used by multiple companies to support custom "business-critical" enterprise solutions. 
This project was born out of my experience dealing with maintaining legacy Winforms applications at previous jobs.

### Purpose and use cases
The Purpose of this project is not make you fall in love with Windows Forms all over again, if you ever loved it, but to help you deal with it when there is way around it :).

### Use Cases
1. Extend a legacy Windows Forms Applications.
2. Create a new Windows Forms Applications. 
3. Isolate Windows Forms Business logic for future migration.
4. Add Automated Unit Testing (Business Logic and State) to a Windows Forms Application.

## Benefits
1. No Specific rules to follow except for enforcing separation of concerns and guarantee re-usability.
2. State and Business logic isolated from Presentation layer.
2. Centralized Form State accessible through the Form View Model.
3. Form Business Logic (Form View) is fully testable.
6. Form State can potentially be shared across forms, persisted and reloaded.

### Rules
1. State should only live in the Form View Model. You should add properties to the Form View Model for each value required to be kept for the life of the form at a global level.
2. State should only be modified by the Form View. The Presentation Layer should never directly modify the View Model State, even if the View Model is exposed on the Form View.
3. All Business logic should be re-factored into the Form View and no business logic should be present in the Presentation or the View Model layer.
4. The Presentation layer should always take care of any presentation specific logic and access the view through the Form View.

_You may follow these rules or not. Formidable will not enforce any of these rules, but if you follow them it will help you better take advantage of the pattern._

### Common Questions
1. **Can I use any Custom or Third-Party Controls?**
   Yes, you can use anything, from Telerik and Infragistics to your custom controls. But, please keep in mind that different control implementations bindings may behave differently.
2. **I am forced to use Bindings and make all View model property raise NotifyPropertyChanged?**
   Well, you should try a abide by that rule if you would like to keep your state, business logic and presentation logic separated. But, Formidable does not force you go in any specific route. The only thing that is set in stone is precisely that, your state, business logic and presentation logic should live in separate entities and the business logic should only live at the View level.
   Another approach is to use the Windows Forms Native Event Pattern and call View methods in order to update and  retrieve values from your state (ViewModel).
3. **What if my application already has an abstract form I have to inherit from?**
   I bet you will find creative ways of resolving this issue...but unfortunately C# does not allow multiple inheritance. I am thinking of ways to go around this. 
4. **Do I have to create all classes in separate files or can they live in the form file?**
   Yes, that is an organizational decision. The only thing to keep in mind when keeping all 4 classes together in the form file is to leave the Partial Form Class at the top, since that is requirement for the designer to work. 
   I use the [Mads Kristensen's File Nesting VS Extension](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.FileNesting) when isolating them in separated files.
5. **What is the WithControl<> method used for?**
   This is tool available in the FormBase that will allow you to perform asynchronous calls without locking your UI. Example: Bind a large dataset and load it into a datagrid while still allow the user to interact with other controls or asynchronously load combo-boxes while the user fills other values.
6. **Is the API stable?**
   Not 100%. The design and described components should not change but there is always room for improvements.
7. **Can it be better?**
   Definitely, there is always room for improvements, but I will always try to keep it as simple as possible.
8. **Will it do everything for me?** Like cooking you a meal or drive you to work? No, definitely not. This framework is intended to provide a foundation, a pattern to follow. Keeping your breakfast from burning or driving on the safe side of the road is still on your hands. 
9. **Nuget Package?**. Not yet...but coming up...

#### Form Components

1. **Form**. 
   This is your form which should always inherit from a custom FormViewPlug.

   Inherits from a **Custom Form Plug**.

   ```csharp
        public partial class MainForm : MainFormViewPlug
        {
            public MainForm() : base()
            {
                InitializeComponent();
                this.BindControls();
            }

            public override void Initialize()
            {
                // After View and View models are created, but before controls are initialized.
                this.View.ViewModel.LabelText = "Enter a text here...";
            }

            private void BindControls()
            {
                this.View.Bind(tbText, "Text", "LabelText");
                this.View.Bind(label2, "Text", "LabelText");
            }
        }
   ```

   You should manage all Presentation specific logic at this level.

2. **Form View Plug**. The Form Plug is a custom form required in order to let you inherit from a Form Base which relies in generics. Unfortunately due to Visual Studio Windows Forms Designer Limitations you are not able to open a Windows Forms that uses generics in design mode. Now, if you do not care about design mode, be my guest and inherit from the FormBase class.

    Inherits from a **FormBase**.

   ```csharp
        public class MainFormPlug : FormBase<MainFormView, MainFormViewModel> 
        {
             public MainFormPlug() : base() 
             {
             }
             
             protected override void initializeForm()
             {
                this.Initialize();
             }

             public virtual void Initialize();
        }
   ```
   This form you be an empty shell. No logic should be placed at this level. You should be able to copy and paste this class only changing the Name and the View and ViewModel Types.

   **API**

   `WithControl<TControl>(TControl, Action<TControl>, bool)` Returns _void_. This method allows you to execute operations on the provided Generic TControl type (of type Control) in an asynchronous manner.

   `View` Returns _Generic Form View Type_. This property returns the Form View instance.

3. **Form View**. The Form View is where all the business logic resides and where all required state modification and retrieval methods and functions should live.

    Inherits from a **FormViewBase**.

   ```csharp
        public class MainFormView : FormViewBase<FormViewModel>
        {
            public MainFormView() : base() 
            {
            }

            protected override void Initialize()
            {
            
            }

            protected override void InitializeOnDesignMode()
            {
            
            }
        }
   ```
   No Presentation specific logic should ever be passed into this level. Example: Do not pass a Control instance or type into any function on the view.

   **API**

   `Initialize()` Returns _void_. This method is executed by the FormViewBase constructor at runtime.
   
   `InitializeOnDesignMode()` Returns _void_. This method is executed by the FormViewBase constructor when in design mode. (Unit Testing).

   `VieModel` Returns _Generic ViewModel Type_. This property returns the ViewModel instance.

   `IsDesignMode()` Returns _Boolean_. This property determines if the FormView is being used in design mode.


4. **Form View Model**. This is where your form state lives. All properties and variables that are affected across your application should live here.
   
   Inherits from a **FormViewModelBase**.
   
   ```csharp
        public class MainFormViewModel : FormViewModelBase
        {
            private string _text;

            public MainFormViewModel() : base()
            {
                this._text = string.Empty;
            }

            public string LabelText
            {
                get
                {
                    return this._text;
                }
                set
                {
                    if (this._text != value)
                    {
                        this._text = value;
                        NotifyPropertyChanged();
                    }
                }
            }

        }
   ```

   **API**

   `ResetState()` Returns _void_. Resets the View Model State.

   `HasChanges()` Returns _Boolean_. Determines if the View Model State was changed.


#### Control Components:

1. **Control**. 
   This is your custom control which should always inherit from a custom ControlViewPlug.

   Inherits from a **Control Plug**.

   ```csharp
        public partial class MembersGridControl : MembersGridPlug
        {
            public MembersGridControl() : base()
            {
                InitializeComponent();
                this.BindControls();
            }

            public override void Initialize()
            {
                // After View and View models are created, but before controls are initialized.
                this.View.ViewModel.Text = "Data";
            }

            private void BindControls()
            {
                this.View.Bind(btnAdd, "Enabled", "HasChanges");
            }
        }
   ```

   You should manage all Presentation specific logic at this level.

2. **Control View Plug**. The Control Plug is a custom form required in order to let you inherit from a Form Base which relies in generics. Unfortunately due to Visual Studio Windows Forms Designer Limitations you are not able to open a Windows Forms that uses generics in design mode. Now, if you do not care about design mode, be my guest and inherit from the ControlBase class.

    Inherits from a **ControlBase**.

   ```csharp
        public class MembersGridPlug : ControlBase<MembersGridView, MembersGridViewModel> 
        {
             public MembersGridPlug() : base() 
             {
             }
             
             protected override void initializeForm()
             {
                this.Initialize();
             }

             public virtual void Initialize();
        }
   ```
   This form you be an empty shell. No logic should be placed at this level. You should be able to copy and paste this class only changing the Name and the View and ViewModel Types.

   **API**

   `WithControl<TControl>(TControl, Action<TControl>, bool)` Returns _void_. This method allows you to execute operations on the provided Generic TControl type (of type Control) in an asynchronous manner.

   `View` Returns _Generic Control View Type_. This property returns the Control View instance.

3. **Control View**. The Control View is where all the business logic resides and where all required state modification and retrieval methods and functions should live.

    Inherits from a **ControlViewBase**.

   ```csharp
        public class MembersGridView : ControlViewBase<MembersGridViewModel>
        {
            public MembersGridView() : base() 
            {
            }

            protected override void Initialize()
            {
            
            }

            protected override void InitializeOnDesignMode()
            {
            
            }
        }
   ```
   No Presentation specific logic should ever be passed into this level. Example: Do not pass a Control instance or type into any function on the view.

   **API**

   `Initialize()` Returns _void_. This method is executed by the ControlViewBase constructor at runtime.
   
   `InitializeOnDesignMode()` Returns _void_. This method is executed by the CViewBase constructor when in design mode. (Unit Testing).

   `VieModel` Returns _Generic ViewModel Type_. This property returns the ViewModel instance.

   `IsDesignMode()` Returns _Boolean_. This property determines if the FormView is being used in design mode.


4. **Control View Model**. This is where your form state lives. All properties and variables that are affected across your application should live here.
   
   Inherits from a **ControlViewModelBase**.
   
   ```csharp
        public class MembersGridViewModel : MembersGridViewModelBase
        {
            private string _text;

            public MainFormViewModel() : base()
            {
                this._text = string.Empty;
            }

            public string LabelText
            {
                get
                {
                    return this._text;
                }
                set
                {
                    if (this._text != value)
                    {
                        this._text = value;
                        NotifyPropertyChanged();
                    }
                }
            }

        }
   ```

   **API**

   `ResetState()` Returns _void_. Resets the View Model State.

   `HasChanges()` Returns _Boolean_. Determines if the View Model State was changed.



### How to use WithControl<>

You should be able to use this method anywhere where you would like your Presentation to make asynchronous calls without locking the main thread hence the UI. It should be fairly easy to use in conjunction with `WithNewTask<>()`
In order to achieve sequential operations on a separate Task it uses a callback design.

`WithNewTask<>` Creates a new task to execute a provided anonymous action, a callback and an exception handler action. The 1st operation is usually to load data or perform any long operation, the second to update the control wrapped into a `WithControl<>` and the third to handle exceptions during execution.

```csharp
        
    // Called From Inside a Form inheriting from the FormPlug or FormBase

    // Creates a New Task (Parallel to main thread)
    this.WithNewTask(() =>
    {
        tsOngoingOperation.Text = $"Loading {this.View.ViewModel.GetMemberNumberInt()} Members...Please wait...";
        
        this.setupGridView();
        // Load Content
        this.View.LoadMembers();
    }, () =>
    {
	    // Load Data into grid without locking the UI
        this.WithControl(this.dgMembers, (gridView) =>
        {
            gridView.DataSource = this.View.ViewModel.Members;
            tsOngoingOperation.Text = $"{this.View.ViewModel.GetMemberNumberInt()} were loaded.";
        });
    }, (ex) =>
    {
        // Exception 
        MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
    });
```

### State Snapshots

**Pending Documentation.**


### .NET Frameworks Compatibility Considerations
- By default he library is provided in .NET 4.0 so it can be used on older solutions, but this has is caveat. You will not be able to take advantage of the CallerMemberName Attribute because it is only available starting on .NET 4.5. The good news is that the solution is designed to be aware of the target .NET framework version and you should be able to make use of this attribute if you compile the Formidable Library to .NET 4.5+.

    .NET 4.0
    ```csharp 
    public string CaptionText {
        get 
        {
            return this._caption;
        }
        set 
        {
            if (this._caption != value) 
            {
                this._caption = value;
                NotifyPropertyChanged("CaptionText");
            }
        }
    }
    ```

    .NET 4.5+
    ```csharp 
    public string CaptionText {
        get 
        {
            return this._caption;
        }
        set 
        {
            if (this._caption != value) 
            {
                this._caption = value;
                // Property Name is auto-detected. Passing the property name is not required.
                NotifyPropertyChanged();
            }
        }
    }
    ```

### Controls Inheritance and Visual Studio Winforms Designer
As you may already know Visual Studio Winforms designer is not very resilient when using form inheritance, specially when the abstract classes live in a separate assembly. When you get a designer exception when opening the form please make sure to follow the steps below:
1. Read the details of the exception.
2. Make sure the Plug Class is not abstract. Designer internals require to be able to instantiate the class so it cannot be abstract.
3. Make sure you are not inheriting from the FormView or ControlView directly. Inheritance from Generic Forms is not supported by the designer.
4. Try Cleaning your solution and rebuilding after making inheritance changes. Make sure to rebuild the assembly where your custom controls live.

This should usually take care of the most common designer exception scenarios.

### Pending:
- More documentation comments and comments in general.
- State Snapshots and sharing documentation.
- Cross-Thread binding without requiring explicit use of WithControl.
- Unit Tests.
- Nicer and better documented Samples :)
- Tweaking...

### Dependencies:

_NONE_

### License

MIT

-----

That's it... I will try to document more scenarios but in general I strive to keep it as simple as possible. 

Godspeed!