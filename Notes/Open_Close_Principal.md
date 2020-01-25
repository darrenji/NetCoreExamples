对扩展开放，对修改封闭。一般适合比较大的框架项目。

```
public class LoanCalculator
{
    private IRateParser rateParser;

    public LoanCalculator(IRateParser rateParser)
    {
        this.rateParser = rateParser;
    }

    public Loan CalculateLoan(LoanDTO LoanDTO)
    {
        Loan loan = new Loan();
        //的确可以通过更多的else if来分别处理。但是，对于比较大的框架、平台或项目这样做不合适
        if(loanDTO.loanType==LoanType.CarLoan)
        {

        }
        else if(loanDTO.loanType == LoanType.HouseLoan)
        {

        }
        else
        {

        }
        return loan;
    }
}
```

设计一个基类做基本的部分。
```
public class LoanCalulator
{
    protected IRateParser rateParser;

    public LoanCalculator(IRateParser rateParser)
    {
        this.rateParser = rateParser;
    }

    public Loan CalculateLoan(LoanDTO loanDTO)
    {
        Loan loan = new Loan();
        //一些基本的处理在这里完成
        return loan;
    }
}
```

然后子类重写基类的方法。
```
public class CarLoanCalculator : LoanCalculator
{
    public CarLoanCalculator(IRateParser rateParser) : base(rateParser)
    {
        base.rateParser = rateParser;
    }

    public override Loan CalculateLoan(LoanDTO loanDTO)
    {
        Loan  loan = new Loan();
        loan.LoanTYpe = loanDTO.loanType;
        loan.InterestRate = rateParser.GetRateByLoanType(loanDTO.LoanType);
        return loan;
    }
}

public class HouseLoanCalculator : LoanCalculator
{
    public HouseLoanCalculator(IRateParser rateParser) : base(rateParser){
        base.rateParser = rateParser;
    }

    public override Loan CalculateLoan(LonDTO loanDTO)
    {
        Loan loan = new Loan();
        loan.LoanType = LoanTYpe.HouseLoan;
        loan.InterestRate = rateParser.GetRateByLoanType(loan.LoanType);
        return loan;
    }
}
```

当然也可以通过接口的方式做。
```
public interface ILoanCalculator
{
    Loan CalculateLoan(LoanDTO loanDTO);
}

public class CarLoanCalculator : ILoanCalculator
{
    private IRateParser rateParser;

    public CarLoanCalculator(IRateParser rateParser)
    {
        this.rateParser = rateParser;
    }

    public Loan CalculatorLoan(LoanDTO loanDTO)
    {
        Loan loan = new Loan();
        loan.LoanType = loanDTO.lOANtYPE;
        loan.InterestRate = rateParser.GetRateByLoanType(loanDTO.LoanTYpe);
        return loan;
    }
}
```