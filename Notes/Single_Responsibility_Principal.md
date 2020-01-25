Single Responsibility Principal, SRP。一个类只有一个理由发生改变，只有一个职责。

```
public class LoanCalculator
{
    public CarLon CalculateCarLoan(LoanDTO loanDTO)
    {
        
    }

    public HouseLoan CalculateHouseLoan(LoanDTO loanDTO)
    {

    }

    public List<Rate> ParseRatesFromXmlString(string xmlString)
    {

    }

    public List<Rate> ParseRtesFromXmlFile(string xmlFile)
    {

    }
}
```
以上两个Parse改变的状态会影响到两个Calculate方法，也就是，同时存在更改状态和读取状态。感觉单一职责的关键在于让状态的改变不能被多个方法影响。应该按如下：

```
public class RateParser : IRateParser
{
    public List<Rate> ParseRatesFromXml(string xmlString)
    {

    }

    public List<Rate> ParseRatesFromXmlFile(string xmlFile)
    {

    }
}

public class LoanCalculator
{
    private IRateParser rateParser;

    public LoanCalculator(IRateParser rateParser)
    {
        this.rateParser = rateParser;
    }

    public CarLoan CalculateCarLoan(LoanDTO loanDTO)
    {

    }

    public HouseLoan CalculateCarLoan(LoanDTO loanDTO)
    {

    }
}
```

以上，外界无法通过其他方式来改变LoanCalculator的状态了。

以上是从状态的改变来看单一职责。以下是从相似性看单一职责。
```
public class LoanRepository
{
    private DbContext dbContext;
    private IEligibilityChecker eligibilityChecker;

    public LoanRepository(DbContext dbContext, IEligibilityChecker eligibilityChecker)
    {
        this.dbContext = dbContext;
        this.eligibilityChecker = eligibilityChecker;
    }

    public List<CarLoan> GetCarLoans()
    {
        return dbContext.CarLoan;
    }

    public List<HouseLoan> GetHouseLoans()
    {
        return dbContext.HouseLoan;
    }

    //上面的两个方法都是关于持久化的，这个方法应该单独出来，因为和持久化无关
    public double CalculateCarLoandRate(CarLoan carLoan)
    {

    }
}
```

把最下面这个方法单独出来。

```
public class LoanService
{
    private IEligibiltyChecker eligibilityChecker;

    public LoanService(IEligibilityChecker eligibilityChecker)
    {
        this.elgigibilityChecker = elgigibilityCheckerj;
    }

    public double CalculatCarLoanRate(DarLoan carLoan)
    {}
}
```

然后Repository变得干净
```
public class LoanRepository
{
    private DbContext dbContext;

    public LoanRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<CarLoan> GetCarLoans()
    {

    }

    public List<HouseLoan> GetHouseLoans()
    {
        
    }
}
```