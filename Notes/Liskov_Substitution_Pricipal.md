```
public class LoanCalculator
{
    public Loan CalculateLoan(LoanDTO loanDTO)
    {

    }
}

public class HouseLoanCalculator : LoanCalculator
{
    public override Loan CalculateLoan(LoanDTO loanDTO)
    {

    }
}

public class CarLoanCalculator : LoanCalculator
{

}
```

然后这样调用。
```
RateParser rateParser = new RateParser();
LoanCalculator loanCalculator = new CarLoanCalculator(rateParser);
```