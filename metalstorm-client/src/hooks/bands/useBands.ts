import axios from "axios";
import { useQuery } from "@tanstack/react-query";
import { BandDto } from "../../dtos";
import { isBefore } from "date-fns";

const fetchBands = async (_limit = 10) => {
    const result = await axios.get("https://localhost:7272/api/bands");
    return result.data;
};
const getBandsByGenreId = async (genreId: string) => {
    const result = await axios.get(
        `https://localhost:7272/api/genres/${genreId}/bands`
    );
    return result.data.sort(
        (a: BandDto, b: BandDto) =>
            isBefore(new Date(a.newestAlbumDate), new Date(b.newestAlbumDate))
                ? 1
                : -1
        // a.name.localeCompare(b.name)
    );
};

const useBands = (limit = 10) => {
    return useQuery(["bands", limit], () => fetchBands(limit));
};
const useGetBandsByGenreId = (genreId: string) => {
    return useQuery(["bandsByGenre", genreId], () =>
        getBandsByGenreId(genreId)
    );
};

export { useBands, useGetBandsByGenreId };
