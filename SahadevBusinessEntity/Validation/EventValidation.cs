using FluentValidation;
using SahadevBusinessEntity.DTO.RequestModel;
using System;

namespace SahadevBusinessEntity.Validation
{

    public class EventValidation : AbstractValidator<RQ_Event>
    {
        public EventValidation()
        {
            RuleFor(x => x.ClientID)
                .NotNull().NotEmpty().WithMessage("Please specify a client id");
                //.LessThan(0).WithMessage("Please specify a valid clientID");
            RuleFor(x => x.EventTypeID)
                .NotNull().NotEmpty().WithMessage("Please specify a event type id");                
            RuleFor(x => x.EventName)
                .NotNull().NotEmpty().WithMessage("Please specify a event name")
                .MaximumLength(80).WithMessage("The lenght of 'name' must not be greater than 80 charatcers");
            RuleFor(x => x.Description)
                .NotNull().NotEmpty().WithMessage("Please specify a description")
                .MaximumLength(400).WithMessage("The lenght of 'description' must not be greater than 400 charatcers");
            RuleFor(x => x.Query)
                .NotNull().NotEmpty().WithMessage("Please specify a query")
                .MaximumLength(400).WithMessage("The lenght of 'query' must not be greater than 400 charatcers");
            RuleFor(x => x.Keywords)
                .NotNull().NotEmpty().WithMessage("Please specify a keywords.")
                .MaximumLength(400).WithMessage("The lenght of 'Keywords' must not be greater than 400 charatcers");
            RuleFor(x => x.StartDate)
                .Must(IsValidDate).WithMessage("Please enter valid start date.");
                //.MaximumLength(80).WithMessage("The lenght of 'Keywords' must not be greater than 400 charatcers");

        }

        private bool IsValidDate(DateTime value)
        {
            if (value == default(DateTime))
                return false;
            return true;
        }


    }
}
