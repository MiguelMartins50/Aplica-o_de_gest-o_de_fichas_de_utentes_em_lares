import React, { useState, useEffect } from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, Text, View, TouchableOpacity, ImageBackground,FlatList } from 'react-native';
import axios from 'axios';
import base64 from 'base64-js';


export default function UtenteFamiliar({ navigation,route  }) {
  const {FamiliarData, FamiliarID} = route.params || {};
  const [UFData, setUFData] = useState([]);
  const [UData, setUData] = useState([]);
  const [imageData, setImageData] = useState(null);


  useEffect(() => {
    axios.get(`http://192.168.1.42:8800/utente_familiar?Familiar_idFamiliar=${FamiliarData.idFamiliar}`)
      .then(consultaResponse => {
        setUFData(consultaResponse.data);
        if (consultaResponse.data && consultaResponse.data[0] && consultaResponse.data[0].Utente_idUtente) {
          const utenteId = consultaResponse.data[0].Utente_idUtente;
  
          axios.get(`http://192.168.1.42:8800/utente?idUtente=${utenteId}`)
            .then(utenteResponse => {
              setUData(utenteResponse.data);
            })
            .catch(error => {
              console.error('Erro ao buscar Consulta do utente:', error);
            });
        } else {
          console.error('Utente_idUtente not found in the response:', consultaResponse.data);
        }
      })
      .catch(error => {
        console.error('Erro ao buscar Consulta do utente familiar:', error);
      });
      console.log('UFData:');

      console.log(UFData);
      console.log('UData:');

      console.log(UData);
    }, [FamiliarData]);
  
    useEffect(() => {
      console.log('Updated UFData:', UFData);
    }, [UFData]);
  
    
    const handleInfo = async () => {
      navigation.navigate('Informação Utente', { UtenteData: UData[0] });
    };
    
  return (
    <View style={styles.container}>
      <FlatList
      data={UData}
      keyExtractor={(item, index) => (item.id ? item.id.toString() : index.toString())}
      renderItem={({ item }) => (
      <View style={styles.View1} key={item.id ? item.id.toString() : null}>
      {item.Imagem && (
        <Image
          style={styles.image}
          source={{ uri: `data:image/png;base64,${base64.fromByteArray(new Uint8Array(item.Imagem.data))}` }}
        />
      )}
      <View style={styles.View2}>
      <Text style={styles.texto}>Nome: {item.nome}</Text>
      <TouchableOpacity style={styles.Button} onPress={handleInfo}>
        <Text style={styles.ButtonText}>Informações do Utente</Text>
      </TouchableOpacity>
      </View>
    </View>
  )}
/>

        <ImageBackground source={require('../Image/Image2.png')} style={[styles.Image2, styles.bottomImage]} />

  </View>
   
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 16,
  
  },
  View1:{
    backgroundColor:'rgba(113, 161, 255, 0.5)',
    padding:10,
    marginTop:20,
    flexDirection: 'row',

  },
  View2:{
   

  },
  Button: {
    backgroundColor: '#3498db',
    paddingVertical: 10,
    paddingHorizontal: 20, 
    borderRadius: 8,
    height: 40,
    width: 150,  
    marginBottom: 10,
    justifyContent: 'center',  
    alignItems: 'center',    
  },
  Text2: {
    paddingVertical: 200,
    paddingHorizontal: 20, 
    borderRadius: 8, 

    justifyContent: 'center',  
    alignItems: 'center',    
  },
  ButtonText: {
    color: '#fff',
    fontSize: 17,
    
  },
  Image1:{
    height:110,
    width:400,
    marginTop:-175
    
    
  },
  Image2: {
    height:205,
    width:410,
    padding:20,
    marginBottom:-150
   
  },
  Imag:{
    padding:50
  },
  Imag1:{
    padding:195,
  },
  sairButton: {
    backgroundColor: 'white',
    padding: 2,
    width:60,
    borderRadius: 5,
    alignSelf: 'flex-end', 
    margin: 40,
  },
  sairButtonText: {
    color: 'black',
    fontWeight: 'bold',
    textAlign:'center'
  },
  ButtonText: {
    color: '#fff',
    fontSize: 10,
    
  },
  image: {
    width: 100, // set width as needed
    height: 100, // set height as needed
    resizeMode: 'cover', // or 'contain' based on your requirement
    borderRadius: 50, // adjust accordingly
  },
});