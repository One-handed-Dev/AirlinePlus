using Common.Application;
using ShopSection.Domain.OrderAgg;
using Common.Application.Contracts;
using ShopSection.Application.Contracts.OrderApp;
using ShopSection.Application.Contracts.FlightApp;

namespace ShopSection.Application.OrderApp
{
    public sealed class OrderApplication : BaseEfApplication<IOrderRepo, ViewOrderDetails, SearchOrder, ViewOrder, Order>, IOrderApplication
    {
        #region Init
        private readonly IFlightApplication roomApplication;
        private readonly IAuthenticationService authenticationService;

        public OrderApplication(IOrderRepo repo, IAuthenticationService authenticationService, IFlightApplication roomApplication) : base(repo)
        {
            this.roomApplication = roomApplication;
            this.authenticationService = authenticationService;
        }
        #endregion

        public void Cancel(long id)
        {
            var order = repo.Get(id);
            order.Cancel();
            repo.Save();
        }

        public long PlaceOrder(Cart cart)
        {
            var currentAccountId = authenticationService.GetCurrentAccountId();

            var order = new Order(currentAccountId, cart.TotalPayAmount);
            cart.Items.ForEach(each => order.Add(new(each.Id, each.Price, each.CountOfFlights, each.CountOfNights, each.StartDate, each.EndDate)));

            repo.Create(order);
            var result = repo.Save();

            //if (result)
            //    order.Items.ForEach(each => roomApplication.UpdateAvailableCountOfFlights(each.FlightId, each.CountOfFlights));

            return order.Id;
        }

        public string PaymentSucceeded(long orderId, string refId)
        {
            var order = repo.Get(orderId);
            order.PaymentSucceeded(refId);

            var symbol = Strings.Symbol.OrderSymbol;
            var issueTrackingNo = CodeGenerator.GenerateRandom(symbol);
            order.SetIssueTrackingNo(issueTrackingNo);

            repo.Save();
            return issueTrackingNo;
        }

        public double GetTotalPayAmountBy(long id) => repo.GetTotalPayAmountBy(id);

        public ViewOrderDetails GetOrderDetailsBy(long orderId) => repo.GetOrderDetailsBy(orderId);
    }
}
