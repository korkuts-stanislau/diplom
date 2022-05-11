using AutoMapper;
using Resource.Data.Interfaces;
using Resource.Models;
using Resource.Tools;
using Resource.UIModels;

namespace Resource.Services;

public class CardsService {
    private readonly IUnitOfWork uof;
    private readonly UserAccessValidator accessValidator;
    private readonly IMapper mapper;

    public CardsService(IUnitOfWork uof, UserAccessValidator accessValidator, IMapper mapper)
    {
        this.uof = uof;
        this.accessValidator = accessValidator;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<CardUI>> GetCardsAsync(string accountId, int projectId) {
        await accessValidator.ValidateAndGetProjectAsync(accountId, projectId);
        return mapper.Map<IEnumerable<CardUI>>(await uof.Cards.GetAsync(projectId));
    }

    public async Task CreateCardAsync(string accountId, int projectId, CardUI card) {
        await accessValidator.ValidateAndGetProjectAsync(accountId, projectId);
        Card newCard = new Card {
            Question = card.Question,
            Answer = card.Answer,
            ProjectId = projectId
        };
        await uof.Cards.CreateAsync(newCard);
        card.Id = newCard.Id;
    }

    public async Task UpdateCardAsync(string accountId, CardUI card) {
        var cardToEdit = await accessValidator.ValidateAndGetCardAsync(accountId, card.Id);
        cardToEdit.Question = card.Question;
        cardToEdit.Answer = card.Answer;
        await uof.Cards.UpdateAsync(cardToEdit);
    }

    public async Task DeleteCardAsync(string accountId, int cardId) {
        var card = await accessValidator.ValidateAndGetCardAsync(accountId, cardId);
        await uof.Cards.DeleteAsync(card);
    }
}