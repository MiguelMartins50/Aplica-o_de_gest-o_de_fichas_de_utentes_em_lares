import React, { useState, useEffect } from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet,Image,Text, View, TextInput, TouchableOpacity, ImageBackground } from 'react-native';



const App = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
    fetch('http://192.168.1.80:8800/tipo') 
      .then(response => response.json())
      .then(data => {
        setData(data);
      })
      .catch(error => console.error('Error fetching data from server', error));
  }, []);

  return (
    <View> 
      <Text></Text>

      <Text>Data from MySQL:</Text>
      {data.map(item => (
        <Text key={item.idTipo}>{`${item.idTipo} | ${item.tipo}`}</Text>
      ))}
    </View>
  );
};
export default App;


const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 16,
    marginBottom:-440
  },
  login: {
    fontSize: 24,
    marginBottom: 24,
    textAlign:'center'
  },
  inputContainer: {
    width: '100%',
    marginBottom: 16,
  },
  input: {
    height: 40,
    borderColor: 'gray',
    borderWidth: 1,
    borderRadius: 8,
    paddingHorizontal: 16,
    marginBottom: 16,
    
  },
  loginButton: {
    backgroundColor: '#3498db',
    paddingVertical: 12,
    paddingHorizontal: 140,
    borderRadius: 8,
    height: 40,
    marginBottom:30
  },
  loginButtonText: {
    color: '#fff',
    fontSize: 16,
    fontWeight: 'bold',
  },
  logo:{
    width: 100,  
    height: 100, 
    resizeMode: 'contain',
    marginBottom: 20,
    alignItems: 'center'
  },
  Image2:{
    padding:100,
    width:400,
  },
  image3:{
    width:358,
    height:350,
    marginTop:-400
  },
  text: {
    color: 'black',
    fontSize: 30,
    lineHeight: 60,
    textAlign: 'center',
    marginTop:110
   
   
  },

  
});

