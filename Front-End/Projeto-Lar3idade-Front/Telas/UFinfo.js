import React, { useState, useEffect } from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, Text, View, TouchableOpacity, ImageBackground,FlatList,ScrollView } from 'react-native';
import base64 from 'base64-js';
import axios from 'axios'; 



export default function UFinfo({ navigation, route }) {
    const UtenteData = route.params && route.params.UtenteData;
    const [imageData, setImageData] = useState(null);
    const [NomeData, setNomeData] = useState(null);
    const [GrauData, setGrauData] = useState(null);
    const [consultaData, setConsultasData] = useState([]);
  
    useEffect(() => {
      axios
        .get(`http://192.168.1.15:8800/consulta?Utente_idUtente=${UtenteData.idUtente}`)
        .then((consultaResponse) => {
          setConsultasData(consultaResponse.data);
        })
        .catch((error) => {
          console.error('Erro ao buscar informação do utente:', error);
        });
    }, []);
  
    useEffect(() => {
      const updateData = async () => {
        if (UtenteData && UtenteData.Imagem && UtenteData.Imagem.data) {
          const base64String = base64.fromByteArray(new Uint8Array(UtenteData.Imagem.data));
          setImageData(`data:image/png;base64,${base64String}`);
        }
  
        if (UtenteData && UtenteData.nome) {
          setNomeData(UtenteData.nome);
        }
  
        if (UtenteData && UtenteData.grau_dependencia) {
          setGrauData(UtenteData.grau_dependencia);
        }
      };
  
      updateData();
    }, [UtenteData]); 
    useEffect(() => {
      console.log(consultaData);
  
    }, [consultaData]);
  
    return (
      <FlatList
        style={styles.container}
        ListHeaderComponent={
          <View style={styles.View1}>
            {imageData && <Image style={styles.image} source={{ uri: imageData }} />}
            <View style={styles.NomeGrauContainer}>
              {NomeData ? <Text style={styles.NomeText}>Nome: {NomeData}</Text> : <Text>Carregando...</Text>}
              {GrauData ? <Text style={styles.GrauText}>Grau de dependencia: {GrauData}</Text> : <Text>Carregando...</Text>}
            </View>
          </View>
        }
        data={consultaData}
        keyExtractor={(item) => item.idConsulta.toString()}
        renderItem={({ item }) => (
          <View style={styles.View3}>
            <Text style={styles.texto}>Médico: {item.nomeMedico}</Text>
            <Text style={styles.texto}>Data e Hora: {new Date(item.data).toLocaleString()}</Text>
            <Text style={styles.texto}>Estado: {item.estado}</Text>
          </View>
        )}
      />
    );
  }

  
const styles = StyleSheet.create({
    container: {  
      flex: 1,
      backgroundColor: '#fff',
      padding: 15,   
    },
    image: {
      width: 100,
      height: 100,
      resizeMode: 'cover',
      borderRadius: 50,
    },
    View1: {
      marginTop: 1,
      padding: 10,
      justifyContent: 'center',
      alignItems: 'center',
    },
    View3: {
      backgroundColor:'rgba(113, 161, 255, 0.5)',
      padding: 10,
      borderWidth: 5,
      borderColor:'white',
      borderRadius: 30,
      justifyContent: 'center',
      alignItems: 'center',
    },
    NomeGrauContainer: {
      marginBottom: 20,
      alignItems: 'center',
    },
    NomeText: {
      fontSize: 18,
      fontWeight: 'bold',
    },
    GrauText: {
      fontSize: 16,
    },
    blackbar: {
      width: 500,
      height: 10,
      backgroundColor: 'black',
      marginTop: 20,
    },
    View2: {
      backgroundColor:'rgba(113, 161, 255, 0.5)',
      padding: 10,
      marginTop: 20,
      width: 300,
      justifyContent: 'center',
      alignItems: 'center',
      borderWidth: 1,
      borderColor: 'black',
      borderRadius: 5,
    },
    texto: {
      fontSize: 16,
      marginBottom: 10,
    },
    Imag: {
      padding: 20,
      marginTop: 20,
    },
    Image2: {
      height: 205,
      width: 410,
      padding: 20,
      marginTop:200,
      marginBottom: -300,
    },
  });