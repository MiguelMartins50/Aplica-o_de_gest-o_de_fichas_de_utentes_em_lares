import express from "express";
import mysql from "mysql";

const app = express()

const db = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: 'ipbcurso',
    database: 'mydb',
  })

app.use(express.json())
app.get("/", (req,res) =>{
    res.json("hello this the backend")
})

app.get("/tipo", (req,res) =>{
    const q = "SELECT * FROM mydb.tipo;"
    db.query(q,(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})

app.get("/funcionario", (req,res) =>{
    const q = "SELECT * FROM mydb.funcionario;"
    db.query(q,(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})

app.get("/utente", (req,res) =>{
    const idUtente = req.query.idUtente;
    let q = "SELECT * FROM mydb.utente"
    if (idUtente) {
        q += ` WHERE idUtente = ${idUtente};`;
    }
    db.query(q,(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})

app.get("/familiar", (req,res) =>{
    const q = "SELECT * FROM mydb.familiar;"
    db.query(q,(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})

app.get("/utente_familiar", (req,res) =>{

    const Familiar_idFamiliar = req.query.Familiar_idFamiliar;

    let q = "SELECT * FROM mydb.utente_familiar"
    if (Familiar_idFamiliar) {
        q += ` WHERE Familiar_idFamiliar = ${Familiar_idFamiliar};`;
    }

    db.query(q,(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})

app.get("/atividade", (req, res) => {
    const Utente_idUtente = req.query.Utente_idUtente;
    let q = "SELECT * FROM mydb.atividade";

    if (Utente_idUtente) {
        q += ` WHERE Utente_idUtente = ${Utente_idUtente}`;
    }

    db.query(q, (err, data) => {
        if (err) return res.json(err);
        return res.json(data);
    });
});

app.get("/consulta", (req, res) => {
    const Utente_idUtente = req.query.Utente_idUtente;


    let q = "SELECT consulta.*, medico.nome AS nomeMedico FROM mydb.consulta JOIN mydb.utente ON consulta.Utente_idUtente = utente.idUtente JOIN mydb.medico ON utente.Medico_idMedico = medico.idMedico ";

    if (Utente_idUtente) {
       
        q += ` WHERE utente.idUtente = ${Utente_idUtente}`;
    } 

    q += " AND consulta.estado = 'Agendada'";


    db.query(q, (err, data) => {
        if (err) return res.json(err);
        return res.json(data);
    });
});

app.get("/escala_medico", (req,res) =>{
    const q = "SELECT * FROM mydb.escala_medico;"
    db.query(q,(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})

app.get("/escala_servico", (req,res) =>{
    const q = "SELECT * FROM mydb.escala_servico;"
    db.query(q,(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})

app.get("/funcionario_escala", (req,res) =>{
    const q = "SELECT * FROM mydb.funcionario_escala;"
    db.query(q,(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})

app.get("/medico", (req,res) =>{
    const q = "SELECT * FROM mydb.medico;"
    db.query(q,(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})

app.get("/pagamento", (req, res) => {
    const Familiar_idFamiliar = req.query.Familiar_idFamiliar;

    let q = `
        SELECT p.data_limitel, p.valor, p.estado
        FROM pagamento p
        JOIN utente u ON p.Utente_idUtente = u.idUtente
        WHERE p.Familiar_idFamiliar = ${Familiar_idFamiliar}
    `;

    db.query(q, (err, data) => {
        if (err) return res.json(err);
        return res.json(data);
    });
});


app.get("/prescricao_medica", (req, res) => {
    const Utente_idUtente = req.query.Utente_idUtente;

    let q = `
        SELECT c.idConsulta, pm.estado, pm.descricao
        FROM prescricao_medica pm
        JOIN consulta c ON pm.Consulta_idConsulta = c.idConsulta
        JOIN utente u ON c.Utente_idUtente = u.idUtente
        WHERE pm.estado = 'ativo'
    `;

    if (Utente_idUtente) {
        q += ` AND u.idUtente = ${Utente_idUtente}`;
    }

    db.query(q, (err, data) => {
        if (err) return res.json(err);
        return res.json(data);
    });
});


app.get("/quarto", (req,res) =>{
    const q = "SELECT * FROM mydb.quarto;"
    db.query(q,(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})

app.get("/tarefa", (req,res) =>{
    const q = "SELECT * FROM mydb.tarefa;"
    db.query(q,(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})


app.get("/visita", (req, res) => {
    const Utente_idUtente = req.query.Utente_idUtente;
    const Familiar_idFamiliar = req.query.Familiar_idFamiliar;
    const Visita_Data = req.query.data;

    let q = `
        SELECT v.idVisita ,f.nomel AS Nome_Familiar,f.idFamiliar, u.idUtente, v.data AS Data_HoraVisita, u.nome AS nomeutente
        FROM visita v
        JOIN familiar f ON v.Familiar_idFamiliar = f.idFamiliar
        JOIN utente u ON v.Utente_idUtente = u.idUtente
    `;

    if (Familiar_idFamiliar) {
        q += ` WHERE f.idFamiliar = ${Familiar_idFamiliar}`;
    }
    if (Utente_idUtente) {
        q += ` WHERE u.idUtente = ${Utente_idUtente}`;
    }
    if (Visita_Data) {
       
        q += ` WHERE v.data = '${Visita_Data}'`;
    }

    db.query(q, (err, data) => {
        if (err) return res.json(err);
        return res.json(data);
    });
});


app.post("/visita", (req,res) =>{
    const q= "INSERT INTO mydb.visita (Utente_idUtente, data ,Familiar_idFamiliar) VALUES (?)"
    const VALUES = [
         req.body.Utente_idUtente,  
        req.body.data,      
        req.body.Familiar_idFamiliar 
    ]
    db.query(q,[VALUES],(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    });
});

app.put("/visita/:id", (req, res) => {
    const q = "UPDATE mydb.visita SET Utente_idUtente = ?, data = ?, Familiar_idFamiliar = ? WHERE idVisita = ?";
    const idVisita = req.params.id;
    const { Utente_idUtente, data, Familiar_idFamiliar } = req.body;

    const VALUES = [Utente_idUtente, data, Familiar_idFamiliar, idVisita];

    db.query(q, VALUES, (err, data) => {
        if (err) return res.json(err);
        return res.json(data);
    });
});

app.delete("/visita/:id", (req, res) => {
    const q = "DELETE FROM mydb.visita WHERE idVisita = ?";
    const idVisita = req.params.id;

    db.query(q, [idVisita], (err, data) => {
        if (err) return res.json(err);
        return res.json(data);
    });
});
app.listen(8800,()=>{
    console.log("connected to backend!")
})