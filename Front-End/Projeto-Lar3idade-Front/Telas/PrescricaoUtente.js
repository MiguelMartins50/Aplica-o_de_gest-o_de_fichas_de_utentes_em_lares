import React, { useEffect, useState } from 'react';
import { StyleSheet, ImageBackground, Text, View, FlatList } from 'react-native'; 
import axios from 'axios'; 

export default function PrescricaoUtente({ route, navigation }) {
  const [PrescricaoData, setPrescricaoData] = useState([]);
  const { utenteData } = route.params;

  useEffect(() => {
    axios.get(`http://192.168.1.92:8800/prescricao_medica?Utente_idUtente=${utenteData.idUtente}`)
    .then(prescricaoResponse => {
      setPrescricaoData(prescricaoResponse.data);
    })
    .catch(error => {
      console.error('Erro ao buscar Prescricao do utente:', error);
    });
  }, []);
  

  return (
    <View style={styles.container}>
      <FlatList
        data={PrescricaoData}
        keyExtractor={(item, index) => (item.id ? item.id.toString() : index.toString())}
        renderItem={({ item }) => (
          <View style={styles.View1} key={item.id ? item.id.toString() : null}>
            <Text style={styles.texto}>ID da consulta: {item.idConsulta}</Text>
            <Text style={styles.texto}>Estado: {item.estado}</Text>
            <Text style={styles.texto}>Descrição: {item.descricao}</Text>
          </View>
        )}
      />
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
  TEXTO: {
    marginBottom: 7,
    borderWidth: 1,
    borderColor: 'black',
    padding: 6,
    width: 'auto',
    borderRadius: 8,
    textAlign: 'center',
  },
  PerfilUtente1: {
    flex: 1,
    padding: 10,
    marginBottom: 50,
    marginTop: 10,
  },
  
  bottomImage: {
    position: 'absolute',
    bottom: -125,
  },
  View1:{
    backgroundColor:'rgba(113, 161, 255, 0.5)',
    padding: 10,
    borderWidth: 5,
    borderColor:'white',
    borderRadius: 30,
    justifyContent: 'center',
    alignItems: 'center',
  },
  texto:{
    marginBottom:10,
    fontSize:15
  }
});
