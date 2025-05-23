using Bank.Domain;
using NUnit.Framework;
using System;

namespace Bank.Domain.Tests
{
    [TestFixture]
    public class BankAccountTests
    {
        [Test]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            var account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            account.Debit(debitAmount);

            // Assert
            Assert.That(account.Balance, Is.EqualTo(expected).Within(0.001), "Account not debited correctly");
        }

        [Test]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            var account = new BankAccount("Mr. Bryan Walton", 11.99);

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(20.0));
            StringAssert.Contains(BankAccount.DebitAmountExceedsBalanceMessage, ex.Message);
        }

        [Test]
        public void Credit_WithPositiveAmount_ShouldIncreaseBalance()
        {
            var account = new BankAccount("Cliente", 100.0);
            account.Credit(50.0);
            Assert.That(account.Balance, Is.EqualTo(150.0).Within(0.001));
        }

        [Test]
        public void Credit_WithZeroAmount_ShouldKeepBalanceUnchanged()
        {
            var account = new BankAccount("Cliente", 100.0);
            account.Credit(0);
            Assert.That(account.Balance, Is.EqualTo(100.0).Within(0.001));
        }

        [Test]
        public void Credit_WithNegativeAmount_ShouldThrowException()
        {
            var account = new BankAccount("Cliente", 100.0);

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Credit(-10));

            // Asegura que el parámetro sea 'amount'
            Assert.That(ex.ParamName, Is.EqualTo("amount"));
        }
    }
}
