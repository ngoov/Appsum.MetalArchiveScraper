import axios from "axios";
import { useQuery } from "react-query";
import { BandDto } from "../../pages/dtos";

const fetchBands = async (_limit = 10) => {
    const result = await axios.get("https://localhost:7272/api/bands");
    return result.data;
};
const getBandsByGenreId = async (genreId: string) => {
    const result = await axios.get(
        `https://localhost:7272/api/genres/${genreId}/bands`
    );
    return result.data.sort((a: BandDto, b: BandDto) =>
        a.name.localeCompare(b.name)
    );
};

const useBands = (limit: number = 10) => {
    return useQuery(["bands", limit], () => fetchBands(limit));
};
const useGetBandsByGenreId = (genreId: string) => {
    return useQuery(["bandsByGenre", genreId], () =>
        getBandsByGenreId(genreId)
    );
};

export { useBands, useGetBandsByGenreId };
