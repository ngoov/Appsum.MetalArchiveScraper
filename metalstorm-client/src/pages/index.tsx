import axios from "axios";
import type { NextPage } from "next";
import Head from "next/head";
import Link from "next/link";
import { useGenres } from "../hooks/albums/useGenres";
import { GenreDto } from "../dtos";

const Home: NextPage = () => {
    axios.defaults.baseURL = "https://localhost:7272/api";
    const { data: genres, isLoading } = useGenres();
    if (isLoading) {
        return <div>Loading...</div>;
    }
    return (
        <div>
            <Head>
                <title>MetalStorm client</title>
                <meta
                    name="description"
                    content="Generated by create next app"
                />
                <link rel="icon" href="/favicon.ico" />
            </Head>
            <input type="number" />
            <div
                style={{
                    fontFamily: "Verdana",
                    display: "grid",
                    gridAutoFlow: "column",
                    gridTemplateRows: "repeat(25, 1fr)",
                }}
            >
                {genres
                    .sort(
                        (a: GenreDto, b: GenreDto) => b.bandCount - a.bandCount
                    )
                    .map((genre: GenreDto) => (
                        <span
                            key={genre.id}
                            style={{
                                padding: ".2rem 0",
                                whiteSpace: "nowrap",
                                overflow: "hidden",
                                wordBreak: "break-all",
                                textOverflow: "ellipsis",
                            }}
                        >
                            <Link
                                href={`/bands?genre=${genre.id}`}
                                key={genre.id}
                                title={`${genre.name} (${genre.bandCount})`}
                            >
                                {genre.name} ({genre.bandCount})
                            </Link>
                        </span>
                    ))}
            </div>
        </div>
    );
};

export default Home;

// export const getStaticProps: GetStaticProps = async () => {
//     const queryClient = new QueryClient();

//     await queryClient.prefetchQuery(["albums"], () => fetchAlbums());

//     return {
//         props: {
//             dehydratedState: dehydrate(queryClient),
//         },
//     };
// };
