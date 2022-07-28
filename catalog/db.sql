--
-- PostgreSQL database dump
--

-- Dumped from database version 14.3
-- Dumped by pg_dump version 14.3

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Product; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Product" (
    "Id" character varying(36) NOT NULL,
    "Name" character varying(50),
    "Weight" double precision,
    "CreationDate" timestamp with time zone,
    "Price" double precision
);


ALTER TABLE public."Product" OWNER TO postgres;

--
-- Data for Name: Product; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Product" ("Id", "Name", "Weight", "CreationDate", "Price") FROM stdin;
5fa4fd13-2f07-4125-8848-6e5dbd57c256	Sneaker_3	12	2022-07-05 21:23:01.847487-03	22.2
20a6048c-f7fb-4b40-bf55-f26246d4f509	Sneaker_2	12	2022-06-30 20:33:48.552232-03	11
8326514b-01dc-45f5-8a5b-ef14665a5268	Sneaker_1	1.5	2022-06-30 00:00:00-03	12
\.


--
-- Name: Product Product_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Product"
    ADD CONSTRAINT "Product_pkey" PRIMARY KEY ("Id");


--
-- PostgreSQL database dump complete
--

