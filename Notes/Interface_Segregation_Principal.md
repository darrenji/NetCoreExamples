```
public interface IRateCalculator
{
    Rate GetYearlyCarLoanRate();
    Rate GetYearlyHouseLoanRate();
    Lender FindLender(LoanType loanType);
}

public class RateCalculator : IRateCalculator
{
    public Rate GetYearlyCarLoanRate(){}
    public Rate GetYearlyHouseLoanRate(){}
    public Lender FindLender(LoanTYpe loanType){}
}
```

把接口拆分。
```
public interface IRateCalculator
{
    Rate GetYearlyCarLoanRate();
    Rate GetYearlyHouseLoanRate();
}

public interface ILenderManager
{
    Lender FindLender(LoanType loanType);
}

public class RateCalculator : IRateCalculator
{
    public Rate GetYearlyCarLoanRate(){}
    public Rate GetYearlyHouseLoanRate(){}
}


```