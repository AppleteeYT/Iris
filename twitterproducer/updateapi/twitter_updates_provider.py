from twitterproducer.tweets.itweets_provider import ITweetsProvider
from twitterproducer.updateapi.update_factory import UpdateFactory
from updatesproducer.iupdates_provider import IUpdatesProvider


class TwitterUpdatesProvider(IUpdatesProvider):
    tweets_provider: ITweetsProvider

    def __init__(self, tweets_provider):
        self.__tweets_provider = tweets_provider

    def get_updates(self, user_id):
        return [
            UpdateFactory.to_update(tweet)
            for tweet in self.__tweets_provider.get_tweets(user_id)
        ]
