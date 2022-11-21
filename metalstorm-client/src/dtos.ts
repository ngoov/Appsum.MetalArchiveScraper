interface BandDto {
    id: string;
    name: string;
    newestAlbumDate: string;
    albums: [];
    genres: [];
}

interface AlbumDto {
    id: string;
    name: string;
    releaseDate: Date;
}
interface GenreDto {
    id: string;
    name: string;
    bandCount: number;
}

export type { BandDto, AlbumDto, GenreDto };
