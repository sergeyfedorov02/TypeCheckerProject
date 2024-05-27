language core;
extend with #type-reconstruction, #multiparameter-functions;

fn do(f : auto) -> auto {
	return fn(c : auto) {
  	return c(c)
  }
}

fn main(f : auto) -> auto {
    return f(f)
}