import axios from "axios";
import { useQuery } from "react-query";

const fetchAlbums = async (_limit = 10) => {
    const result = await axios.get("https://localhost:7272/api");
    return result.data;
};

const useAlbums = (limit: number) => {
    return useQuery(["albums", limit], () => fetchAlbums(limit));
};

export { useAlbums as usePosts, fetchAlbums };
