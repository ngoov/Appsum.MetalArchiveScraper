import axios from "axios";
import { useQuery } from "react-query";

const fetchAlbums = async (_limit = 10) => {
    const result = await axios.get("https://localhost:7272/api/albums");
    return result.data;
};

const useAlbums = (limit: number = 10) => {
    return useQuery(["albums", limit], () => fetchAlbums(limit));
};

export { useAlbums };
