namespace AllSpice.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;
  private readonly FavoritesService _favoritesService;

  public AccountService(AccountsRepository repo, FavoritesService favoritesService)
  {
    _repo = repo;
    _favoritesService = favoritesService;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name?.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture?.Length > 0 ? editData.Picture : original.Picture;
    return _repo.Edit(original);
  }

  internal List<MyFavorite> GetMyFavorites(string userId)
  {
    List<MyFavorite> favorites = _favoritesService.GetMyFavorites(userId);
    return favorites;
  }
}
