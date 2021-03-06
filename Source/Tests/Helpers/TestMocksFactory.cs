using System;
using System.Collections.Generic;
using Moq;
using Terminals.Data;

namespace Tests.Helpers
{
    internal static class TestMocksFactory
    {
        internal static Mock<IPersistence> CreatePersistence()
        {
            var persistenceStub = new Mock<IPersistence>();
            persistenceStub.SetupGet(p => p.Security).Returns(new PersistenceSecurity());
            var credentials = new Mock<ICredentials>();
            credentials.Setup(cr => cr.GetEnumerator()).Returns(new List<ICredentialSet>().GetEnumerator());
            persistenceStub.SetupGet(p => p.Credentials).Returns(credentials.Object);
            var groupsStub = new Mock<IGroups>();
            groupsStub.Setup(g => g.GetEnumerator()).Returns(new List<IGroup>().GetEnumerator());
            persistenceStub.SetupGet(persistence => persistence.Groups).Returns(groupsStub.Object);
            var favoritesStub = new Mock<IFavorites>();
            persistenceStub.SetupGet(p => p.Favorites).Returns(favoritesStub.Object);
            var factoryStub = new Mock<IFactory>();
            persistenceStub.SetupGet(p => p.Factory).Returns(factoryStub.Object);

            return persistenceStub;
        }

        internal static Favorite CreateFavorite()
        {
            return CreateFavorite(new List<IGroup>());
        }

        internal static Favorite CreateFavorite(List<IGroup> groups)
        {
            var favoriteGroupsStub = new Mock<IFavoriteGroups>();
            favoriteGroupsStub.Setup(fg => fg.GetGroupsContainingFavorite(It.IsAny<Guid>())).Returns(groups);
            var favorite = new Favorite();
            favorite.AssignStores(favoriteGroupsStub.Object);
            return favorite;
        }
    }
}