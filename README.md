
# FluentValidation.Extensions.Br
> An extension of the fluent validation with a set of Brazilian validations


[![Build status](https://ci.appveyor.com/api/projects/status/497l6ojfocc7v4m0?svg=true)](https://ci.appveyor.com/project/LucasMendesl/fluentvalidation-extensions-br) [![Nuget](http://img.shields.io/nuget/v/Extensions.FluentValidation.Br.svg?maxAge=10800)](https://www.nuget.org/packages/Extensions.FluentValidation.Br/)

## Main Goal
The prupose of this library is create a extension with brazilian´s validation to [FluentValidation](https://github.com/JeremySkinner/FluentValidation) package.

## Description
This library was designed to provide a set of brazilian´s validation (CPF/CNPJ) avoiding duplicate code.    

## How to Use ?
```csharp
public class Person 
{
    public string Name { get; set; }
    public string CPF  { get; set; }
    public string CNPJ { get; set; }
}

public class PersonValidator : AbstractValidator<Person>
{
  public PersonValidator ()
  {
      RuleFor(employee => employee.Name).NotNull();
      RuleFor(employee => employee.CPF).IsValidCPF();
      RuleFor(employee => employee.CNPJ).IsValidCNPJ();
  }
}
```
## Contributing
Contributions via pull requests are welcome :-).
