PGDMP                         z            PayCoreFinalWork    14.5    14.5 c    ?           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            ?           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            ?           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            ?           1262    16459    PayCoreFinalWork    DATABASE     o   CREATE DATABASE "PayCoreFinalWork" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Turkish_Turkey.1254';
 "   DROP DATABASE "PayCoreFinalWork";
                postgres    false                        2615    16502    hangfire    SCHEMA        CREATE SCHEMA hangfire;
    DROP SCHEMA hangfire;
                postgres    false            ?            1259    16509    counter    TABLE     ?   CREATE TABLE hangfire.counter (
    id bigint NOT NULL,
    key text NOT NULL,
    value bigint NOT NULL,
    expireat timestamp without time zone
);
    DROP TABLE hangfire.counter;
       hangfire         heap    postgres    false    5            ?            1259    16508    counter_id_seq    SEQUENCE     y   CREATE SEQUENCE hangfire.counter_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE hangfire.counter_id_seq;
       hangfire          postgres    false    216    5            ?           0    0    counter_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE hangfire.counter_id_seq OWNED BY hangfire.counter.id;
          hangfire          postgres    false    215            ?            1259    16517    hash    TABLE     ?   CREATE TABLE hangfire.hash (
    id bigint NOT NULL,
    key text NOT NULL,
    field text NOT NULL,
    value text,
    expireat timestamp without time zone,
    updatecount integer DEFAULT 0 NOT NULL
);
    DROP TABLE hangfire.hash;
       hangfire         heap    postgres    false    5            ?            1259    16516    hash_id_seq    SEQUENCE     v   CREATE SEQUENCE hangfire.hash_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE hangfire.hash_id_seq;
       hangfire          postgres    false    5    218            ?           0    0    hash_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE hangfire.hash_id_seq OWNED BY hangfire.hash.id;
          hangfire          postgres    false    217            ?            1259    16528    job    TABLE     '  CREATE TABLE hangfire.job (
    id bigint NOT NULL,
    stateid bigint,
    statename text,
    invocationdata text NOT NULL,
    arguments text NOT NULL,
    createdat timestamp without time zone NOT NULL,
    expireat timestamp without time zone,
    updatecount integer DEFAULT 0 NOT NULL
);
    DROP TABLE hangfire.job;
       hangfire         heap    postgres    false    5            ?            1259    16527 
   job_id_seq    SEQUENCE     u   CREATE SEQUENCE hangfire.job_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE hangfire.job_id_seq;
       hangfire          postgres    false    220    5            ?           0    0 
   job_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE hangfire.job_id_seq OWNED BY hangfire.job.id;
          hangfire          postgres    false    219            ?            1259    16588    jobparameter    TABLE     ?   CREATE TABLE hangfire.jobparameter (
    id bigint NOT NULL,
    jobid bigint NOT NULL,
    name text NOT NULL,
    value text,
    updatecount integer DEFAULT 0 NOT NULL
);
 "   DROP TABLE hangfire.jobparameter;
       hangfire         heap    postgres    false    5            ?            1259    16587    jobparameter_id_seq    SEQUENCE     ~   CREATE SEQUENCE hangfire.jobparameter_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE hangfire.jobparameter_id_seq;
       hangfire          postgres    false    5    231            ?           0    0    jobparameter_id_seq    SEQUENCE OWNED BY     O   ALTER SEQUENCE hangfire.jobparameter_id_seq OWNED BY hangfire.jobparameter.id;
          hangfire          postgres    false    230            ?            1259    16553    jobqueue    TABLE     ?   CREATE TABLE hangfire.jobqueue (
    id bigint NOT NULL,
    jobid bigint NOT NULL,
    queue text NOT NULL,
    fetchedat timestamp without time zone,
    updatecount integer DEFAULT 0 NOT NULL
);
    DROP TABLE hangfire.jobqueue;
       hangfire         heap    postgres    false    5            ?            1259    16552    jobqueue_id_seq    SEQUENCE     z   CREATE SEQUENCE hangfire.jobqueue_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE hangfire.jobqueue_id_seq;
       hangfire          postgres    false    224    5            ?           0    0    jobqueue_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE hangfire.jobqueue_id_seq OWNED BY hangfire.jobqueue.id;
          hangfire          postgres    false    223            ?            1259    16561    list    TABLE     ?   CREATE TABLE hangfire.list (
    id bigint NOT NULL,
    key text NOT NULL,
    value text,
    expireat timestamp without time zone,
    updatecount integer DEFAULT 0 NOT NULL
);
    DROP TABLE hangfire.list;
       hangfire         heap    postgres    false    5            ?            1259    16560    list_id_seq    SEQUENCE     v   CREATE SEQUENCE hangfire.list_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE hangfire.list_id_seq;
       hangfire          postgres    false    226    5            ?           0    0    list_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE hangfire.list_id_seq OWNED BY hangfire.list.id;
          hangfire          postgres    false    225            ?            1259    16602    lock    TABLE     ?   CREATE TABLE hangfire.lock (
    resource text NOT NULL,
    updatecount integer DEFAULT 0 NOT NULL,
    acquired timestamp without time zone
);
    DROP TABLE hangfire.lock;
       hangfire         heap    postgres    false    5            ?            1259    16503    schema    TABLE     ?   CREATE TABLE hangfire.schema (
    version integer NOT NULL
);
    DROP TABLE hangfire.schema;
       hangfire         heap    postgres    false    5            ?            1259    16569    server    TABLE     ?   CREATE TABLE hangfire.server (
    id text NOT NULL,
    data text,
    lastheartbeat timestamp without time zone NOT NULL,
    updatecount integer DEFAULT 0 NOT NULL
);
    DROP TABLE hangfire.server;
       hangfire         heap    postgres    false    5            ?            1259    16577    set    TABLE     ?   CREATE TABLE hangfire.set (
    id bigint NOT NULL,
    key text NOT NULL,
    score double precision NOT NULL,
    value text NOT NULL,
    expireat timestamp without time zone,
    updatecount integer DEFAULT 0 NOT NULL
);
    DROP TABLE hangfire.set;
       hangfire         heap    postgres    false    5            ?            1259    16576 
   set_id_seq    SEQUENCE     u   CREATE SEQUENCE hangfire.set_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE hangfire.set_id_seq;
       hangfire          postgres    false    229    5            ?           0    0 
   set_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE hangfire.set_id_seq OWNED BY hangfire.set.id;
          hangfire          postgres    false    228            ?            1259    16538    state    TABLE     ?   CREATE TABLE hangfire.state (
    id bigint NOT NULL,
    jobid bigint NOT NULL,
    name text NOT NULL,
    reason text,
    createdat timestamp without time zone NOT NULL,
    data text,
    updatecount integer DEFAULT 0 NOT NULL
);
    DROP TABLE hangfire.state;
       hangfire         heap    postgres    false    5            ?            1259    16537    state_id_seq    SEQUENCE     w   CREATE SEQUENCE hangfire.state_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE hangfire.state_id_seq;
       hangfire          postgres    false    222    5            ?           0    0    state_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE hangfire.state_id_seq OWNED BY hangfire.state.id;
          hangfire          postgres    false    221            ?            1259    16495    accounts    TABLE     ?  CREATE TABLE public.accounts (
    id uuid NOT NULL,
    firstname character varying(40) NOT NULL,
    lastname character varying(40) NOT NULL,
    username character varying(25) NOT NULL,
    email character varying(255) NOT NULL,
    password character varying(500) NOT NULL,
    phonenumber character varying(20),
    age integer,
    lastactivity timestamp without time zone,
    role character varying(255)
);
    DROP TABLE public.accounts;
       public         heap    postgres    false            ?            1259    16463 
   categories    TABLE     ?   CREATE TABLE public.categories (
    id uuid NOT NULL,
    name character varying(50) NOT NULL,
    description character varying(1000)
);
    DROP TABLE public.categories;
       public         heap    postgres    false            ?            1259    16490    offers    TABLE     ?   CREATE TABLE public.offers (
    id uuid NOT NULL,
    accountid uuid NOT NULL,
    productid uuid NOT NULL,
    price double precision,
    comment character varying(255),
    isaccepted boolean,
    offereduserid uuid
);
    DROP TABLE public.offers;
       public         heap    postgres    false            ?            1259    16470    products    TABLE     c  CREATE TABLE public.products (
    id uuid NOT NULL,
    name character varying(100) NOT NULL,
    description character varying(500) NOT NULL,
    color character varying(255) NOT NULL,
    brand character varying(255) NOT NULL,
    price double precision NOT NULL,
    categoryid uuid,
    issold boolean,
    isofferable boolean,
    accountid uuid
);
    DROP TABLE public.products;
       public         heap    postgres    false            ?           2604    16635 
   counter id    DEFAULT     l   ALTER TABLE ONLY hangfire.counter ALTER COLUMN id SET DEFAULT nextval('hangfire.counter_id_seq'::regclass);
 ;   ALTER TABLE hangfire.counter ALTER COLUMN id DROP DEFAULT;
       hangfire          postgres    false    216    215    216            ?           2604    16644    hash id    DEFAULT     f   ALTER TABLE ONLY hangfire.hash ALTER COLUMN id SET DEFAULT nextval('hangfire.hash_id_seq'::regclass);
 8   ALTER TABLE hangfire.hash ALTER COLUMN id DROP DEFAULT;
       hangfire          postgres    false    217    218    218            ?           2604    16654    job id    DEFAULT     d   ALTER TABLE ONLY hangfire.job ALTER COLUMN id SET DEFAULT nextval('hangfire.job_id_seq'::regclass);
 7   ALTER TABLE hangfire.job ALTER COLUMN id DROP DEFAULT;
       hangfire          postgres    false    220    219    220            ?           2604    16704    jobparameter id    DEFAULT     v   ALTER TABLE ONLY hangfire.jobparameter ALTER COLUMN id SET DEFAULT nextval('hangfire.jobparameter_id_seq'::regclass);
 @   ALTER TABLE hangfire.jobparameter ALTER COLUMN id DROP DEFAULT;
       hangfire          postgres    false    231    230    231            ?           2604    16727    jobqueue id    DEFAULT     n   ALTER TABLE ONLY hangfire.jobqueue ALTER COLUMN id SET DEFAULT nextval('hangfire.jobqueue_id_seq'::regclass);
 <   ALTER TABLE hangfire.jobqueue ALTER COLUMN id DROP DEFAULT;
       hangfire          postgres    false    224    223    224            ?           2604    16747    list id    DEFAULT     f   ALTER TABLE ONLY hangfire.list ALTER COLUMN id SET DEFAULT nextval('hangfire.list_id_seq'::regclass);
 8   ALTER TABLE hangfire.list ALTER COLUMN id DROP DEFAULT;
       hangfire          postgres    false    225    226    226            ?           2604    16756    set id    DEFAULT     d   ALTER TABLE ONLY hangfire.set ALTER COLUMN id SET DEFAULT nextval('hangfire.set_id_seq'::regclass);
 7   ALTER TABLE hangfire.set ALTER COLUMN id DROP DEFAULT;
       hangfire          postgres    false    229    228    229            ?           2604    16681    state id    DEFAULT     h   ALTER TABLE ONLY hangfire.state ALTER COLUMN id SET DEFAULT nextval('hangfire.state_id_seq'::regclass);
 9   ALTER TABLE hangfire.state ALTER COLUMN id DROP DEFAULT;
       hangfire          postgres    false    222    221    222            o          0    16509    counter 
   TABLE DATA           =   COPY hangfire.counter (id, key, value, expireat) FROM stdin;
    hangfire          postgres    false    216   ?n       q          0    16517    hash 
   TABLE DATA           N   COPY hangfire.hash (id, key, field, value, expireat, updatecount) FROM stdin;
    hangfire          postgres    false    218   ?p       s          0    16528    job 
   TABLE DATA           t   COPY hangfire.job (id, stateid, statename, invocationdata, arguments, createdat, expireat, updatecount) FROM stdin;
    hangfire          postgres    false    220   ?p       ~          0    16588    jobparameter 
   TABLE DATA           M   COPY hangfire.jobparameter (id, jobid, name, value, updatecount) FROM stdin;
    hangfire          postgres    false    231   ?t       w          0    16553    jobqueue 
   TABLE DATA           N   COPY hangfire.jobqueue (id, jobid, queue, fetchedat, updatecount) FROM stdin;
    hangfire          postgres    false    224   ~u       y          0    16561    list 
   TABLE DATA           G   COPY hangfire.list (id, key, value, expireat, updatecount) FROM stdin;
    hangfire          postgres    false    226   ?u                 0    16602    lock 
   TABLE DATA           A   COPY hangfire.lock (resource, updatecount, acquired) FROM stdin;
    hangfire          postgres    false    232   ?u       m          0    16503    schema 
   TABLE DATA           +   COPY hangfire.schema (version) FROM stdin;
    hangfire          postgres    false    214   ?u       z          0    16569    server 
   TABLE DATA           H   COPY hangfire.server (id, data, lastheartbeat, updatecount) FROM stdin;
    hangfire          postgres    false    227   ?u       |          0    16577    set 
   TABLE DATA           M   COPY hangfire.set (id, key, score, value, expireat, updatecount) FROM stdin;
    hangfire          postgres    false    229   v       u          0    16538    state 
   TABLE DATA           X   COPY hangfire.state (id, jobid, name, reason, createdat, data, updatecount) FROM stdin;
    hangfire          postgres    false    222   /v       l          0    16495    accounts 
   TABLE DATA           |   COPY public.accounts (id, firstname, lastname, username, email, password, phonenumber, age, lastactivity, role) FROM stdin;
    public          postgres    false    213   ?y       i          0    16463 
   categories 
   TABLE DATA           ;   COPY public.categories (id, name, description) FROM stdin;
    public          postgres    false    210   J{       k          0    16490    offers 
   TABLE DATA           e   COPY public.offers (id, accountid, productid, price, comment, isaccepted, offereduserid) FROM stdin;
    public          postgres    false    212   ?{       j          0    16470    products 
   TABLE DATA           z   COPY public.products (id, name, description, color, brand, price, categoryid, issold, isofferable, accountid) FROM stdin;
    public          postgres    false    211   i|       ?           0    0    counter_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('hangfire.counter_id_seq', 126, true);
          hangfire          postgres    false    215            ?           0    0    hash_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('hangfire.hash_id_seq', 1, false);
          hangfire          postgres    false    217            ?           0    0 
   job_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('hangfire.job_id_seq', 29, true);
          hangfire          postgres    false    219            ?           0    0    jobparameter_id_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('hangfire.jobparameter_id_seq', 61, true);
          hangfire          postgres    false    230            ?           0    0    jobqueue_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('hangfire.jobqueue_id_seq', 29, true);
          hangfire          postgres    false    223            ?           0    0    list_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('hangfire.list_id_seq', 1, false);
          hangfire          postgres    false    225            ?           0    0 
   set_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('hangfire.set_id_seq', 3, true);
          hangfire          postgres    false    228            ?           0    0    state_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('hangfire.state_id_seq', 91, true);
          hangfire          postgres    false    221            ?           2606    16637    counter counter_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY hangfire.counter
    ADD CONSTRAINT counter_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY hangfire.counter DROP CONSTRAINT counter_pkey;
       hangfire            postgres    false    216            ?           2606    16772    hash hash_key_field_key 
   CONSTRAINT     Z   ALTER TABLE ONLY hangfire.hash
    ADD CONSTRAINT hash_key_field_key UNIQUE (key, field);
 C   ALTER TABLE ONLY hangfire.hash DROP CONSTRAINT hash_key_field_key;
       hangfire            postgres    false    218    218            ?           2606    16646    hash hash_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY hangfire.hash
    ADD CONSTRAINT hash_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY hangfire.hash DROP CONSTRAINT hash_pkey;
       hangfire            postgres    false    218            ?           2606    16656    job job_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY hangfire.job
    ADD CONSTRAINT job_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY hangfire.job DROP CONSTRAINT job_pkey;
       hangfire            postgres    false    220            ?           2606    16706    jobparameter jobparameter_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY hangfire.jobparameter
    ADD CONSTRAINT jobparameter_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY hangfire.jobparameter DROP CONSTRAINT jobparameter_pkey;
       hangfire            postgres    false    231            ?           2606    16729    jobqueue jobqueue_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY hangfire.jobqueue
    ADD CONSTRAINT jobqueue_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY hangfire.jobqueue DROP CONSTRAINT jobqueue_pkey;
       hangfire            postgres    false    224            ?           2606    16749    list list_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY hangfire.list
    ADD CONSTRAINT list_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY hangfire.list DROP CONSTRAINT list_pkey;
       hangfire            postgres    false    226            ?           2606    16628    lock lock_resource_key 
   CONSTRAINT     W   ALTER TABLE ONLY hangfire.lock
    ADD CONSTRAINT lock_resource_key UNIQUE (resource);
 B   ALTER TABLE ONLY hangfire.lock DROP CONSTRAINT lock_resource_key;
       hangfire            postgres    false    232            ?           2606    16507    schema schema_pkey 
   CONSTRAINT     W   ALTER TABLE ONLY hangfire.schema
    ADD CONSTRAINT schema_pkey PRIMARY KEY (version);
 >   ALTER TABLE ONLY hangfire.schema DROP CONSTRAINT schema_pkey;
       hangfire            postgres    false    214            ?           2606    16775    server server_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY hangfire.server
    ADD CONSTRAINT server_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY hangfire.server DROP CONSTRAINT server_pkey;
       hangfire            postgres    false    227            ?           2606    16777    set set_key_value_key 
   CONSTRAINT     X   ALTER TABLE ONLY hangfire.set
    ADD CONSTRAINT set_key_value_key UNIQUE (key, value);
 A   ALTER TABLE ONLY hangfire.set DROP CONSTRAINT set_key_value_key;
       hangfire            postgres    false    229    229            ?           2606    16758    set set_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY hangfire.set
    ADD CONSTRAINT set_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY hangfire.set DROP CONSTRAINT set_pkey;
       hangfire            postgres    false    229            ?           2606    16683    state state_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY hangfire.state
    ADD CONSTRAINT state_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY hangfire.state DROP CONSTRAINT state_pkey;
       hangfire            postgres    false    222            ?           2606    16501    accounts accounts_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.accounts
    ADD CONSTRAINT accounts_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.accounts DROP CONSTRAINT accounts_pkey;
       public            postgres    false    213            ?           2606    16469    categories categories_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.categories
    ADD CONSTRAINT categories_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.categories DROP CONSTRAINT categories_pkey;
       public            postgres    false    210            ?           2606    16494    offers offers_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.offers
    ADD CONSTRAINT offers_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.offers DROP CONSTRAINT offers_pkey;
       public            postgres    false    212            ?           2606    16476    products products_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.products DROP CONSTRAINT products_pkey;
       public            postgres    false    211            ?           1259    16619    ix_hangfire_counter_expireat    INDEX     V   CREATE INDEX ix_hangfire_counter_expireat ON hangfire.counter USING btree (expireat);
 2   DROP INDEX hangfire.ix_hangfire_counter_expireat;
       hangfire            postgres    false    216            ?           1259    16766    ix_hangfire_counter_key    INDEX     L   CREATE INDEX ix_hangfire_counter_key ON hangfire.counter USING btree (key);
 -   DROP INDEX hangfire.ix_hangfire_counter_key;
       hangfire            postgres    false    216            ?           1259    16784    ix_hangfire_hash_expireat    INDEX     P   CREATE INDEX ix_hangfire_hash_expireat ON hangfire.hash USING btree (expireat);
 /   DROP INDEX hangfire.ix_hangfire_hash_expireat;
       hangfire            postgres    false    218            ?           1259    16781    ix_hangfire_job_expireat    INDEX     N   CREATE INDEX ix_hangfire_job_expireat ON hangfire.job USING btree (expireat);
 .   DROP INDEX hangfire.ix_hangfire_job_expireat;
       hangfire            postgres    false    220            ?           1259    16773    ix_hangfire_job_statename    INDEX     P   CREATE INDEX ix_hangfire_job_statename ON hangfire.job USING btree (statename);
 /   DROP INDEX hangfire.ix_hangfire_job_statename;
       hangfire            postgres    false    220            ?           1259    16778 %   ix_hangfire_jobparameter_jobidandname    INDEX     g   CREATE INDEX ix_hangfire_jobparameter_jobidandname ON hangfire.jobparameter USING btree (jobid, name);
 ;   DROP INDEX hangfire.ix_hangfire_jobparameter_jobidandname;
       hangfire            postgres    false    231    231            ?           1259    16738 "   ix_hangfire_jobqueue_jobidandqueue    INDEX     a   CREATE INDEX ix_hangfire_jobqueue_jobidandqueue ON hangfire.jobqueue USING btree (jobid, queue);
 8   DROP INDEX hangfire.ix_hangfire_jobqueue_jobidandqueue;
       hangfire            postgres    false    224    224            ?           1259    16631 &   ix_hangfire_jobqueue_queueandfetchedat    INDEX     i   CREATE INDEX ix_hangfire_jobqueue_queueandfetchedat ON hangfire.jobqueue USING btree (queue, fetchedat);
 <   DROP INDEX hangfire.ix_hangfire_jobqueue_queueandfetchedat;
       hangfire            postgres    false    224    224            ?           1259    16782    ix_hangfire_list_expireat    INDEX     P   CREATE INDEX ix_hangfire_list_expireat ON hangfire.list USING btree (expireat);
 /   DROP INDEX hangfire.ix_hangfire_list_expireat;
       hangfire            postgres    false    226            ?           1259    16783    ix_hangfire_set_expireat    INDEX     N   CREATE INDEX ix_hangfire_set_expireat ON hangfire.set USING btree (expireat);
 .   DROP INDEX hangfire.ix_hangfire_set_expireat;
       hangfire            postgres    false    229            ?           1259    16792    ix_hangfire_set_key_score    INDEX     Q   CREATE INDEX ix_hangfire_set_key_score ON hangfire.set USING btree (key, score);
 /   DROP INDEX hangfire.ix_hangfire_set_key_score;
       hangfire            postgres    false    229    229            ?           1259    16691    ix_hangfire_state_jobid    INDEX     L   CREATE INDEX ix_hangfire_state_jobid ON hangfire.state USING btree (jobid);
 -   DROP INDEX hangfire.ix_hangfire_state_jobid;
       hangfire            postgres    false    222            ?           1259    16779    jobqueue_queue_fetchat_jobid    INDEX     f   CREATE INDEX jobqueue_queue_fetchat_jobid ON hangfire.jobqueue USING btree (queue, fetchedat, jobid);
 2   DROP INDEX hangfire.jobqueue_queue_fetchat_jobid;
       hangfire            postgres    false    224    224    224            ?           2606    16715 $   jobparameter jobparameter_jobid_fkey    FK CONSTRAINT     ?   ALTER TABLE ONLY hangfire.jobparameter
    ADD CONSTRAINT jobparameter_jobid_fkey FOREIGN KEY (jobid) REFERENCES hangfire.job(id) ON UPDATE CASCADE ON DELETE CASCADE;
 P   ALTER TABLE ONLY hangfire.jobparameter DROP CONSTRAINT jobparameter_jobid_fkey;
       hangfire          postgres    false    220    3267    231            ?           2606    16692    state state_jobid_fkey    FK CONSTRAINT     ?   ALTER TABLE ONLY hangfire.state
    ADD CONSTRAINT state_jobid_fkey FOREIGN KEY (jobid) REFERENCES hangfire.job(id) ON UPDATE CASCADE ON DELETE CASCADE;
 B   ALTER TABLE ONLY hangfire.state DROP CONSTRAINT state_jobid_fkey;
       hangfire          postgres    false    222    3267    220            o   ?  x???͍1??Vn?ERE????????C9??????G>???????>?????W' ?A??/xy>?q%?B???@s,?(?Jp???]???bY*?]????բZQ??_)??Qkx#???s?:`g?.??u?l?Bɼ ????J9#?$UM04Y??7?o?p?p:??M5??Q*??KV>7?Du??%?????Uۆ??n?N?P??M8Yf|U?W????+???O?? ?eO/?i?c?y?:^U?m?V?H????s???;Zm͢E?s??l~???lئۮ??}?,???WN???9dÓ?<h?ldNh@f?ܦw????Ru??$4TOB?@u?V? ???L?s:V??m??;u??a???9???P\>~]?D??ufo?K???.?s?c?6L?"?Y4???55r??????: A?b?M???T?f???\?g?S???5????5)? ??/???yV??'ar?fU#.????d?      q      x?????? ? ?      s     x??YMo?8=+???%???"?x?????!?Q{Q,?(ib??G@I?E???{?=7??-E9Q?X???|??pD?{?F?IO??6)| ?@?ܙ.ϡ?v?|y?
x&<z???>,?0?,ӏ????>?y????"?a4??b&?"	???N????_w:'???@?lI????1<??E??L????]?(O녨?r??^2?G?H̊??*??S??L??w?= ? f???6+??~????I?}?ߘ????2??9S?i???????i!?y!?9? ?? ???d?YEK? ??ҹ??(˗???ßy7 ??4q?L;?H´??4(?? ????T:x=rQ??g???????.e?׵?iw?1m???ޠ?	t8x#???M!???R?.??%???o??P?$??b.<??p?F??$?????d??2?q=>???y???KX}??m???Vٞ
I?"?F?}Q<??}(?W??04?]?QV???*?
Ueo?|L4UvkK;5?	?b?K??k?.?t??ضjQ&?bG'?Rjkx?PͲ[?{<??"eUrc9?n???)^???"<?&v???p??[1M5??^U?T???[??Rv3?q??_[???(a??n????BE?=?`\???s.0?9??@?Fqm??D?s??cD?z?.f:1,?!7??2?3?+?i??
????Q?Ic???$?kP???,??S!y;Ksē?0?Rܾ??
+?N?bK!N??^U? of׽:~?MZ޲穰gW)???M?6%?t??%??[??Z%?G??ʩ??H.dkA?'??K??k???Ǉ???{"C)[?ݨ?y? zS?T????c???E??p?=?7??MJs?????`?~??.-???t????r?<\???}??I?[??,????????ܩmv?:<uh?]K?gD?]??\J??Mq6?]i???l?f?h??6??nv$ᮭ?|u?Nȗi?T??*N?
?DF
Χ???H????$nI?3??b?̱?;?M?,S?`??O?????ߠޅ      ~   ?   x?}?1?0????3nVi?$m?v?? :??b??$?I??j?άy???h???1?>???rS\?w?2ˇA8??L?ˢ_?????K??????pn?!\??1?cW?????laҰ,NZ????E???X???????mͲ?O+??!~?      w      x?????? ? ?      y      x?????? ? ?            x?????? ? ?      m      x?34??????       z      x?????? ? ?      |      x?????? ? ?      u   ?  x??????6?ϙ?=???DѷE[????s?e.?,m????],??K%?dkg/?!HbY??#i?? m???q??:l?n?A?E?{׃?0z?????t??y?om?-K?6??n?S[??C???y?q7L-???/?px?=-+??8????0?i?\@ל
<???:}??Ү>?]?A.?0?h?z2?f?9?K??_?ӯ/??8?VOd?Z0??LLI1cL???{?A?y?Wz?W??zT??i???$?M?m?{???-)?_???U????]?=??S?N7?O?=???}]?z??-w?9??e?K???G??????Z?ξC?}?*?·@1.S? ???F?[?/Pm
L?"Փ ?H(H?c2Z?h?`$R2?	c???tv[M>K?u??f??띟?GQ?u???iW?7?)5?.?????ܕ??
???C?b??'M?Ɏ?d~?w???v???0?????D?`X.{?wꇻ???b??H?Ax?t?m?WJ??@m  0^*?&1???q,????,z69??	Lf(??+C?Ż??	?: J6?p??MtޚABՕ?i|I6?˰^݋?>?2-?Ob?Wi?X??%e?No^?Ž????<?u?G?Z?E-?:??????q??U@N??oXQ?e i??T??F?F?L??5???8^???n4#?F?e0?? ??0Xn????UXg??l???"?????\o?(r??eX:??X?=aǞUw?G=7?,n???:+???	??,r?۪???`?D???Ug??rv\p??%,?D3?Gc!??DŤ???ǘ|-_`?f?
???:	?:k???	???k/`!?4??d??Jz?NE??
+???e??@????/}B????ŏ?}?????_C%??(??5T??w]y?R?De???????? ????      l   j  x?m??N?0????_?m?v-????d oJ?6???`.?,??? /?b?????N??/??U?)d]!?M?????*Ick???????p????????꭭@r?I??d?7Eה[?O???}?o?-??x?6K??s?????04w?4[<???œ?XO?ǥ??g}??"??b樂4???Qr& w%T2(S??B?@,{T?0ﶛ??{[u?j?I???#?Q??J0?\?c?QE8?`X???????Vu?? ??;?]???׭?\???(???C??c?xx??z4<NFa?s>K??y??<[:ِ???,v??M4ʂ?49????9?n?!R??k$?GE??]"=ᱳ????t~ /i??      i   s   x?M?1? ?9?"pE$??????CU?4??۷ST??qMeZ?# J>G?
H??2?????????ο?Y????ACZ*??g???Y+???m?6???`??+???a??1?i?'?      k   ?   x???1c?
7??$R|zq????߂?M3XZ!A?\??	ڪB?y?:?JQ??Z??X??G3m?n?$Ge({P?Sꨰ?P?8y?z)?y|???~ݟ?}?Hۦ(۱?????L??piV{m?y??-?,?      j   ?   x???1n?0Eg?? I?(??Pi<8?S??ޜ?Uhѥ?Pp??H??f?@y -$?.>vw???r?[?whR?1A?!%???f?se4f1agns??\?2???A?1@/?9֐B??aV,#ԅƵڠ$,бbA??^ܫ޷ӱ?Ǿ?I????:>????????V]Η??&??$ｋV{?AUP????~??4&?????i?>?Afg     