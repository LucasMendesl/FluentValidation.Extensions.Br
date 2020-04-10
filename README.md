
# FluentValidation.Extensions.Br

[![Build status](https://ci.appveyor.com/api/projects/status/497l6ojfocc7v4m0?svg=true)](https://ci.appveyor.com/project/LucasMendesl/fluentvalidation-extensions-br) 
An extension of the fluent validation with a set of Brazilian validations

## Main Goal
The prupose of this library is create a extension with brazilian´s validation to [FluentValidation](https://github.com/JeremySkinner/FluentValidation) package.

## Description
This library was designed to provide a set of brazilian´s validation (CPF/CNPJ/PIS) avoiding duplicate code.    

## How to Use ?
```csharp
public class Person 
{
    public string Name { get; set; }
    public string CPF  { get; set; }
	public string PIS { get; set; }
}

public class PersonValidator : AbstractValidator<Person>
{
  public PersonValidator ()
  {
      RuleFor(employee => employee.Name).NotNull();
      RuleFor(employee => employee.CPF).IsValidCPF();
	  RuleFor(employee => employee.PIS).IsValidPIS();
  }
}
```
## Contributing
Contributions via pull requests are welcome :-).
