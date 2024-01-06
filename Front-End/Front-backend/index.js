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

app.post("/tipo", (req,res) =>{
    const q= "INSERT INTO tipo (`tipo`) VALUES (?)"
    const VALUES = [
        req.body.tipo
    ]
    db.query(q,[VALUES],(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})

app.get("/tipo", (req,res) =>{
    const q = "SELECT * FROM mydb.tipo;"
    db.query(q,(err,data)=>{
        if(err) return res.json(err)
        return res.json(data)
    })
})
app.listen(8800,()=>{
    console.log("connected to backend!")
})