using Bank.Domain;
using NUnit.Framework;

namespace Bank.Domain.Tests
{
    public class BankAccountTests
    {
        [Test]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act
            account.Debit(debitAmount);
            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }
        [Test]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(BankAccount.DebitAmountExceedsBalanceMessage, e.Message);
            }
        }
        [TestMethod]
        public void Credit_WithPositiveAmount_ShouldIncreaseBalance()
        {
            // Arrange
            var account = new BankAccount("Cliente", 100.0);
            
            // Act
            account.Credit(50.0);

            // Assert
            Assert.AreEqual(150.0, account.Balance, 0.001);
        }

        [TestMethod]
        public void Credit_WithZeroAmount_ShouldKeepBalanceUnchanged()
        {
            var account = new BankAccount("Cliente", 100.0);
            account.Credit(0);
            Assert.AreEqual(100.0, account.Balance, 0.001);
        }

        [TestMethod]
        public void Credit_WithNegativeAmount_ShouldThrowException()
        {
            var account = new BankAccount("Cliente", 100.0);
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Credit(-10));
            StringAssert.Contains(ex.Message, "amount");
        }
        
    }
}