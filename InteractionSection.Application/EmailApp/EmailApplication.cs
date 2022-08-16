using System;
using MimeKit;
using Common.Application;
using ShopSection.Domain.OrderAgg;
using AccountSection.Domain.AccountAgg;
using CommentSection.Domain.CommentAgg;
using InteractionSection.Domain.EmailAgg;
using AccountSection.Domain.AccountPolicyAgg;
using AccountSection.Domain.ForgetPasswordLogAgg;
using InteractionSection.Application.Contracts.EmailApp;

namespace InteractionSection.Application.EmailApp
{
    public class EmailApplication : BaseEfApplication<IEmailRepo, SaveEmail, SearchEmail, ViewEmail, Email>, IEmailApplication
    {
        #region Init
        private readonly IOrderRepo orderRepo;
        private readonly IAccountRepo accountRepo;
        private readonly ICommentRepo commentRepo;
        private readonly IAccountPolicyRepo accountPolicyRepo;
        private readonly EmailConfigurationDto emailConfigurationDto;
        private readonly IForgetPasswordLogRepo forgetPasswordLogRepo;

        public EmailApplication(IEmailRepo repo, EmailConfigurationDto emailConfiguration, ICommentRepo commentRepo, IOrderRepo orderRepo, IAccountRepo accountRepo, IForgetPasswordLogRepo forgetPasswordLogRepo, IAccountPolicyRepo accountPolicyRepo) : base(repo)
        {
            this.orderRepo = orderRepo;
            this.commentRepo = commentRepo;
            this.accountRepo = accountRepo;
            this.accountPolicyRepo = accountPolicyRepo;
            this.emailConfigurationDto = emailConfiguration;
            this.forgetPasswordLogRepo = forgetPasswordLogRepo;
        }
        #endregion

        public TaskResult Send(EmailDto command)
        {
            var emailMessage = CreateEmailMessage(command);
            return DoSend(emailMessage);
        }

        public TaskResult SendOrderNotif(long orderId)
        {
            TaskResult task = new();
            var target = orderRepo.Get(orderId);

            if (target is null) return task.Failed(TaskResult.Messages.RecordNotFound);

            var account = accountRepo.Get(target.AccountId);
            if (account is null) return task.Failed(TaskResult.Messages.RecordNotFound);

            var ownerEmail = account.Email;

            if (string.IsNullOrWhiteSpace(ownerEmail)) return task.Failed(TaskResult.Messages.NotEnoughInformation);

            var ownerName = account.Fullname;
            var emailSubject = "تاییدیه ثبت رزرو در سایت رزرواسیون هتل میزبان";
            var emailContent = $@"کاربر گرامی، {ownerName} سفر خوشی را برایتان آرزومندیم.
                                شناسه رزرو شما: {target.IssueTrackingNo}
                                مبلغ پرداختی شما: {target.TotalPayAmount.ToMoney()} تومان";

            EmailDto emailDto = new(new() { ownerEmail }, emailSubject, emailContent.ConvertDigitsToPersian());
            var result = Send(emailDto);

            if (result.IsSuccedded) Create(new(emailSubject, emailContent.ConvertDigitsToPersian(), ownerName, ownerEmail));

            return result;
        }

        public TaskResult SendLogInNotif(long accountId)
        {
            TaskResult task = new();
            var target = accountRepo.Get(accountId);

            if (target is null) return task.Failed(TaskResult.Messages.RecordNotFound);

            var ownerEmail = target.Email;

            if (string.IsNullOrWhiteSpace(ownerEmail)) return task.Failed(TaskResult.Messages.NotEnoughInformation);

            var ownerName = target.Fullname;
            var emailSubject = "اعلان ورود به حساب کاربری در سایت رزرواسیون هتل میزبان";
            var emailContent = $@"کاربر گرامی، {ownerName} در ساعت {DateTime.Now.ToJalaliFullString()} به سایت میزبان واردشدید.";

            EmailDto emailDto = new(new() { ownerEmail }, emailSubject, emailContent.ConvertDigitsToPersian());
            var result = Send(emailDto);

            if (result.IsSuccedded) Create(new(emailSubject, emailContent, ownerName, ownerEmail));

            return result;
        }

        private TaskResult DoSend(MimeMessage mailMessage)
        {
            TaskResult task = new();
            using var client = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                client.Connect(emailConfigurationDto.SmtpServer, emailConfigurationDto.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(emailConfigurationDto.UserName, emailConfigurationDto.Password);
                client.Send(mailMessage);
                return task.Succedded();
            }

            catch (Exception e)
            {
                return task.Failed(e.Message);
            }

            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public TaskResult SendSignUpNotif(string phoneNumber)
        {
            TaskResult task = new();
            var target = accountRepo.GetByPhoneNumber(phoneNumber);

            if (target is null) return task.Failed(TaskResult.Messages.RecordNotFound);

            var ownerEmail = target.Email;

            if (string.IsNullOrWhiteSpace(ownerEmail)) return task.Failed(TaskResult.Messages.NotEnoughInformation);

            var ownerName = target.Fullname;
            var emailSubject = "تاییدیه ثبت حساب کاربری در سایت رزرواسیون هتل میزبان";
            var emailContent = $@"کاربر گرامی، {ownerName} به سایت میزبان خوش آمدید.";

            EmailDto emailDto = new(new() { ownerEmail }, emailSubject, emailContent);
            var result = Send(emailDto);

            if (result.IsSuccedded) Create(new(emailSubject, emailContent.ConvertDigitsToPersian(), ownerName, ownerEmail));

            return result;
        }

        public TaskResult SendCommentFeedback(long commentId)
        {
            TaskResult task = new();
            var target = commentRepo.Get(commentId);

            if (target is null) return task.Failed(TaskResult.Messages.RecordNotFound);
            if (string.IsNullOrWhiteSpace(target.Feedback)) return task.Failed(TaskResult.Messages.NotEnoughInformation);

            var account = accountRepo.Get(target.OwnerId);
            if (account is null) return task.Failed(TaskResult.Messages.RecordNotFound);

            var ownerEmail = account.Email;

            if (string.IsNullOrWhiteSpace(ownerEmail)) return task.Failed(TaskResult.Messages.NotEnoughInformation);

            var ownerName = account.Fullname;
            var emailSubject = "پاسخ به نظر شما در سایت رزرواسیون هتل میزبان";
            var emailContent = $@"کاربر گرامی، {ownerName} به نظر شما پاسخ داده شد.
                                نظر شما: {target.Content}
                                و پاسخ ما: {target.Feedback}";

            EmailDto emailDto = new(new() { ownerEmail }, emailSubject, emailContent);
            var result = Send(emailDto);

            if (result.IsSuccedded) Create(new(emailSubject, emailContent.ConvertDigitsToPersian(), ownerName, ownerEmail));

            return result;
        }

        private MimeMessage CreateEmailMessage(EmailDto command)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(emailConfigurationDto.From, emailConfigurationDto.From));
            emailMessage.To.AddRange(command.To);
            emailMessage.Subject = command.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = command.Content };

            return emailMessage;
        }

        public TaskResult SendRecoverPasswordLink(string phoneNumber)
        {
            TaskResult task = new();
            var target = accountRepo.GetByPhoneNumber(phoneNumber);

            if (target is null) return task.Failed(TaskResult.Messages.RecordNotFound);

            var ownerEmail = target.Email;

            if (string.IsNullOrWhiteSpace(ownerEmail)) return task.Failed(TaskResult.Messages.NotEnoughInformation);

            var ownerName = target.Fullname;
            var link = accountPolicyRepo.Get().ResetPasswordCallbackUrl + forgetPasswordLogRepo.GetValidHash(x => x.PhoneNumber == phoneNumber);
            var emailSubject = "بازیابی رمزعبور حساب سایت رزرواسیون هتل میزبان";
            var emailContent = $@"کاربر گرامی، از لینک روبرو اقدام به بازیابی رمزعبور خود کنید: {link}";

            EmailDto emailDto = new(new() { ownerEmail }, emailSubject, emailContent);
            var result = Send(emailDto);

            if (result.IsSuccedded) Create(new(emailSubject, emailContent.ConvertDigitsToPersian(), ownerName, ownerEmail));

            return result;
        }

        public override TaskResult Edit(SaveEmail command) => Edit(command, default);

        public override TaskResult Create(SaveEmail command) => Create(command, default);
    }
}
