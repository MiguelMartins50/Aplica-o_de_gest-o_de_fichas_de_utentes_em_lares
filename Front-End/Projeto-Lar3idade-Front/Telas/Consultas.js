import React, { useEffect, useState } from 'react';
import { StyleSheet, ImageBackground, Text, View, FlatList } from 'react-native'; 
import axios from 'axios'; 
import { iduten } from './Utente';
export default function Consultas({ route, navigation }) {
  const [consultaData, setConsultasData] = useState([]);

  useEffect(() => {
    axios.get(`http://192.168.1.80:8800/consulta?Utente_idUtente=${iduten}`)
    .then(consultaResponse => {
      setConsultasData(consultaResponse.data);
    })
    .catch(error => {
      console.error('Erro ao buscar Consulta do utente:', error);
    });
  }, []);
  

  return (
    <View style={styles.container}>
      <FlatList
        data={consultaData}
        keyExtractor={(item, index) => (item.id ? item.id.toString() : index.toString())}
        renderItem={({ item }) => (
          <View style={styles.View1} key={item.id ? item.id.toString() : null}>
            <Text style={styles.texto}>Médico: {item.nomeMedico}</Text>
            <Text style={styles.texto}>Data e Hora: {new Date(item.data).toLocaleString()}</Text>
            <Text style={styles.texto}>Estado: {item.estado}</Text>
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
