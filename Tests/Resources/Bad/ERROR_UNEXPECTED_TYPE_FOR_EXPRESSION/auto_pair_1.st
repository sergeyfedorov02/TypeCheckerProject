language core;
extend with #type-reconstruction, #tuples, #multiparameter-functions;

fn do1(a : auto) -> auto {
	return succ(a)
}

fn do2(a : auto) -> fn(auto) -> auto {
	return fn (n : auto) {
  	return { Nat::iszero(n), do1(a) }
  }
}

fn main(n : Bool) -> auto {
	return do2(n)(n)
}