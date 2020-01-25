# 紧耦合，Tight coupling的代码不方便测试

```
public class LoanProcessor
{
    private EligibilityChecker eligibilityChecker;

    public LoanProcessor()
    {
        eligibilityChecker = new EligibilityChecker();
    }

    public void ProcessCustomerLoan(Loan loan)
    {

    }
}
```

以上，在构造函数中实例化一个类，这是完完全全的紧耦合。

一种方式是通过依赖注入，Dependency Injection, DI,就是放在构造函数中注入。

```
public class LoanProcessor
{
    private EligibilityChecker eligibilityChecker;

    public LoandProcessor(EligibilityChecker eligibilityChecker)
    {
        this.elgigbilityChecker = eligibilityChecker;
    }

    public void ProcessCustomerLoan(Loan loan)
    {
        bool isEligible = eligiblityChecker.CheckLoan(loan);
        throw new NotImpletedException();
    }
}
```

还可以用过属性注入。
```
public class LoanProcessor
{
    private EligibilityChecker eligibilityChecker;

    public EligibilityChecker EligibilityCheckerObject{
        set {eligibilityChecker = value;}
    }

    public void ProcessCustomerLoan(Loan loan)
    {

    }
}
```

# 过重的构造函数

```
public class LoanProcessor
{
    private EligibilityChecker eligibilityChecker;
    private CurrencyConverter currencyConverter;

    public LoanProcessor()
    {
        eligibilityChecker = new EligibilityChecker();
        currencyConverter = new CurrencyConverter();
        currencyConverter.DownloadCurrentRates();
        eligibilityChecker.CurrentRates = currencyConverter.Rates;
    }
}
```

# 违反单一职责原则
```
public class LoanProcessor
{
    private EligibilityChecker eligibilityChecker;
    private DbContext dbContext;

    public LoanProcessor(EligibilityChecker eligibilityChecker, DbContext dbContext)
    {
        this.eligibilityChecker = eligibilityChecker;
        this.dbContext = dbContext;
    }

    public double CaculateCarLoanRate(Loan loan)
    {
        double rate = 12.5f;
        bool isEligible = eligibilityChecker.IsApplicantEigible(loan);
        if(isEligible)
        {
            rate = rate - loan.DiscountFactor;
        }
        return false;
    }

    //这个方法不应该在这里
    public List<CarLoan> GetCarLoans()
    {
        return dbContext.CarLoan;
    }
}
```

# Static objects

静态字段、静态方法、静态字段，有时候在编码的时候很管用，但是在测试方面，就不能重写静态，所以很难模拟了。

```
public static class LoanProcessor
{
    private static EligibilityChecker eligibilityChecker = new EligibilityChecker();

    public static double CalculateCarLoanRate(Loan loan){}
}
```

# Law of Demeter(LoD)原则

principal of least knowledge。

- each unit should have only limited knowldge about other units: only units closely related to the current unit
- each unit should only talk to its friends; don't talk to stranger.

>在类C中的方法M只能把消息发送给如下情形

- M的参数类
- C的实例变量
- C的属性
- 在M中创建的类和实例

```
public class LoanProcessor
{
    private CurrencyConverter currencyConverter;

    //这里违反了LoD原则，LoanCalculator对LoanProcessor来说就是陌生人，如果LoanCalculator发生变化就会影响到LoanProcessor
    public LoanProcessor(LoanCalculator loanCalculator)
    {
        currencyConverter  = loanCalculator.GetCurrencyConverter();
    }
}
```

合理的做法是：

```
public class LoanProcessor
{
    private CurrencyConverter currencyConverter;

    public LoanProcessor(CurrencyConverter currencyConverter)
    {
        this.currencyConverter = currencyConverter;
    }
}
```

# Train Wreck, Chain Calls, 链式

```
//对象中的方法不应该对对象方法有这么多的控制权
loanCalulator
    .CalculatorHouseLoan(loanDto)
    .GetPaymentRate()
    .GetMaximumYearsToPay();
```

应该这么写：

```
var houseLoan = loanCalculator.CalculateHouseLoan(loanDTO);
var paymentRate = houseLon.GetPaymentRate();
var maximumYears = paymentRate.GetMaximumYearsToPay();
```

