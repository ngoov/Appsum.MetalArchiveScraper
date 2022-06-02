interface BandDto {
    id: string;
    name: string;
    albums: [];
    genres: [];
}

interface AlbumDto {
    id: string;
    name: string;
}
interface GenreDto {
    id: string;
    name: string;
    bandCount: number;
}

export type { BandDto, AlbumDto, GenreDto };
