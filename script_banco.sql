CREATE TABLE "Setores" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Setores" PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT NOT NULL
);

CREATE TABLE "Prioridades" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Prioridades" PRIMARY KEY AUTOINCREMENT,
    "Descricao" TEXT NOT NULL,
    "PrazoHoras" INTEGER NOT NULL
);

CREATE TABLE "Chamados" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Chamados" PRIMARY KEY AUTOINCREMENT,
    "Descricao" TEXT NOT NULL,
    "SetorId" INTEGER NOT NULL,
    "PrioridadeId" INTEGER NOT NULL,
    "Status" TEXT NOT NULL DEFAULT 'Aberto',
    "DataAbertura" TEXT NOT NULL,
    "DataCheckin" TEXT NULL,
    "DataCheckout" TEXT NULL,
    "Solucao" TEXT NULL,
    CONSTRAINT "FK_Chamados_Prioridades_PrioridadeId" FOREIGN KEY ("PrioridadeId") REFERENCES "Prioridades" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Chamados_Setores_SetorId" FOREIGN KEY ("SetorId") REFERENCES "Setores" ("Id") ON DELETE CASCADE
);

INSERT INTO "Setores" ("Id", "Nome") VALUES (1, 'TI'), (2, 'RH'), (3, 'Financeiro'), (4, 'Manutenção');
INSERT INTO "Prioridades" ("Id", "Descricao", "PrazoHoras") VALUES (1, 'Baixa', 48), (2, 'Média', 24), (3, 'Alta', 4);