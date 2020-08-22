from datetime import datetime

from updatesproducer.updateapi.media import Media
from updatesproducer.updateapi.mediatype import MediaType
from updatesproducer.updateapi.update import Update
from youtubeproducer.videos.video import Video

YOUTUBE_BASE_URL = 'https://www.youtube.com'
ISO8601 = datetime.now().replace(microsecond=0).isoformat()

class UpdateFactory:
    @staticmethod
    def to_update(video: Video):
        url = f'{YOUTUBE_BASE_URL}/watch?v={video.video_id}'
        return Update(
            content=f'{video.title}\n{video.description}',
            author_id=video.channelId,
            creation_date=datetime.strptime(video.publishedAt, "%Y-%m-%dT%H:%M:%SZ"),
            url=url,
            media=[
                Media(url, MediaType.Video)
            ],
            repost=False
        )